import mysql.connector
from mysql.connector import Error
import datetime

class DatabaseManager:
    def __init__(self, host, database, user, password):
        self.host = host
        self.database = database
        self.user = user
        self.password = password
        self.conn = None
        self.cursor = None
        self.connect()

    def connect(self):
        try:
            self.conn = mysql.connector.connect(
                host=self.host,
                database=self.database,
                user=self.user,
                password=self.password
            )
            if self.conn.is_connected():
                self.cursor = self.conn.cursor()
                print(f"Kết nối thành công tới CSDL MySQL: {self.database}")
        except Error as e:
            print(f"Lỗi kết nối CSDL MySQL: {e}")
            self.conn = None

    def close(self):
        if self.conn and self.conn.is_connected():
            self.cursor.close()
            self.conn.close()
            print("Đã đóng kết nối CSDL MySQL.")

    def create_tables(self):
        if not self.conn or not self.conn.is_connected():
            print("Chưa kết nối CSDL, không thể tạo bảng.")
            return

        try:
            self.cursor.execute("""
                CREATE TABLE IF NOT EXISTS students (
                    mssv VARCHAR(50) PRIMARY KEY,
                    ten VARCHAR(255) NOT NULL,
                    ngay_sinh DATE,
                    email VARCHAR(255) UNIQUE,
                    dien_thoai VARCHAR(20),
                    dia_chi TEXT
                ) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_unicode_ci;
            """)

            self.cursor.execute("""
                CREATE TABLE IF NOT EXISTS courses (
                    ma_khoa_hoc VARCHAR(50) PRIMARY KEY,
                    ten_khoa_hoc VARCHAR(255) NOT NULL,
                    mo_ta TEXT,
                    so_tin_chi INT
                ) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_unicode_ci;
            """)

            self.cursor.execute("""
                CREATE TABLE IF NOT EXISTS enrollment (
                    ma_dang_ky VARCHAR(50) PRIMARY KEY,
                    mssv VARCHAR(50) NOT NULL,
                    ma_khoa_hoc VARCHAR(50) NOT NULL,
                    ngay_dang_ky DATE,
                    FOREIGN KEY (mssv) REFERENCES students(mssv) ON DELETE CASCADE,
                    FOREIGN KEY (ma_khoa_hoc) REFERENCES courses(ma_khoa_hoc) ON DELETE CASCADE
                ) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_unicode_ci;
            """)
            self.conn.commit()
            print("Đã tạo các bảng thành công (nếu chưa tồn tại).")
        except Error as e:
            print(f"Lỗi khi tạo bảng: {e}")

    def execute_query(self, query, params=()):
        if not self.conn or not self.conn.is_connected():
            print("Chưa kết nối CSDL.")
            return None
        try:
            self.cursor.execute(query, params)
            if query.strip().upper().startswith(("INSERT", "UPDATE", "DELETE")):
                self.conn.commit()
                return self.cursor.rowcount
            else:
                return self.cursor.fetchall()
        except Error as e:
            if self.conn and self.conn.is_connected():
                self.conn.rollback()
            print(f"Lỗi khi thực thi câu lệnh SQL: {e}")
            return None

class SinhVien:
    def __init__(self, mssv, ten, ngay_sinh, email, dien_thoai, dia_chi):
        self.mssv = mssv
        self.ten = ten
        if isinstance(ngay_sinh, datetime.date):
            self.ngay_sinh = ngay_sinh.strftime("%Y-%m-%d")
        else:
            self.ngay_sinh = ngay_sinh

        self.email = email
        self.dien_thoai = dien_thoai
        self.dia_chi = dia_chi

    def __str__(self):
        return (f"MSSV: {self.mssv}, Tên: {self.ten}, Ngày sinh: {self.ngay_sinh}, "
                f"Email: {self.email}, ĐT: {self.dien_thoai}, Địa chỉ: {self.dia_chi}")

class KhoaHoc:
    def __init__(self, ma_khoa_hoc, ten_khoa_hoc, mo_ta, so_tin_chi):
        self.ma_khoa_hoc = ma_khoa_hoc
        self.ten_khoa_hoc = ten_khoa_hoc
        self.mo_ta = mo_ta
        self.so_tin_chi = so_tin_chi

    def __str__(self):
        return (f"Mã KH: {self.ma_khoa_hoc}, Tên KH: {self.ten_khoa_hoc}, "
                f"Mô tả: {self.mo_ta}, Số tín chỉ: {self.so_tin_chi}")

class DangKy:
    def __init__(self, ma_dang_ky, mssv, ma_khoa_hoc, ngay_dang_ky=None):
        self.ma_dang_ky = ma_dang_ky
        self.mssv = mssv
        self.ma_khoa_hoc = ma_khoa_hoc
        if ngay_dang_ky:
            if isinstance(ngay_dang_ky, datetime.date):
                self.ngay_dang_ky = ngay_dang_ky.strftime("%Y-%m-%d")
            else:
                self.ngay_dang_ky = ngay_dang_ky
        else:
            self.ngay_dang_ky = datetime.date.today().strftime("%Y-%m-%d")

    def __str__(self):
        return (f"Mã ĐK: {self.ma_dang_ky}, MSSV: {self.mssv}, "
                f"Mã KH: {self.ma_khoa_hoc}, Ngày ĐK: {self.ngay_dang_ky}")


class QuanLyDangKyKhoaHoc:
    def __init__(self):
        self.db_manager = DatabaseManager(
            host="localhost",
            database="python",
            user="root",
            password="taolao"
        )
        if not self.db_manager.conn or not self.db_manager.conn.is_connected():
            print("Không thể khởi tạo ứng dụng do lỗi kết nối CSDL. Vui lòng kiểm tra lại cấu hình MySQL.")
            exit()

    def __del__(self):
        self.db_manager.close()

    def them_sinh_vien(self):
        print("\n--- Thêm Sinh Viên ---")
        mssv = input("Nhập MSSV: ")
        if self.db_manager.execute_query("SELECT * FROM students WHERE mssv = %s", (mssv,)):
            print(f"Lỗi: MSSV {mssv} đã tồn tại.")
            return

        ten = input("Nhập tên: ")
        ngay_sinh = input("Nhập ngày sinh (YYYY-MM-DD): ")
        try:
            datetime.date.fromisoformat(ngay_sinh)
        except ValueError:
            print("Lỗi: Định dạng ngày sinh không hợp lệ. Vui lòng nhập theo YYYY-MM-DD.")
            return

        email = input("Nhập email: ")
        dien_thoai = input("Nhập số điện thoại: ")
        dia_chi = input("Nhập địa chỉ: ")

        sinh_vien = SinhVien(mssv, ten, ngay_sinh, email, dien_thoai, dia_chi)
        query = """
            INSERT INTO students (mssv, ten, ngay_sinh, email, dien_thoai, dia_chi)
            VALUES (%s, %s, %s, %s, %s, %s)
        """
        params = (sinh_vien.mssv, sinh_vien.ten, sinh_vien.ngay_sinh,
                  sinh_vien.email, sinh_vien.dien_thoai, sinh_vien.dia_chi)
        if self.db_manager.execute_query(query, params) == 1:
            print("Thêm sinh viên thành công!")
        else:
            print("Thêm sinh viên thất bại.")

    def sua_sinh_vien(self):
        print("\n--- Sửa Thông Tin Sinh Viên ---")
        mssv = input("Nhập MSSV của sinh viên cần sửa: ")
        sv_hien_tai = self.db_manager.execute_query("SELECT * FROM students WHERE mssv = %s", (mssv,)).copy()
        if not sv_hien_tai:
            print(f"Không tìm thấy sinh viên với MSSV: {mssv}")
            return
        
        sv_hien_tai = sv_hien_tai[0] 
        print("Thông tin hiện tại:")
        print(f"MSSV: {sv_hien_tai[0]}, Tên: {sv_hien_tai[1]}, NS: {sv_hien_tai[2]}, Email: {sv_hien_tai[3]}, ĐT: {sv_hien_tai[4]}, ĐC: {sv_hien_tai[5]}")

        ten = input(f"Nhập tên mới (hiện tại: {sv_hien_tai[1]}, bỏ qua nếu không đổi): ") or sv_hien_tai[1]
        
        ngay_sinh_input = input(f"Nhập ngày sinh mới (YYYY-MM-DD) (hiện tại: {sv_hien_tai[2]}, bỏ qua nếu không đổi): ")
        ngay_sinh = str(sv_hien_tai[2]) if sv_hien_tai[2] else ""
        if ngay_sinh_input:
            try:
                datetime.date.fromisoformat(ngay_sinh_input)
                ngay_sinh = ngay_sinh_input
            except ValueError:
                print("Lỗi: Định dạng ngày sinh không hợp lệ. Giữ nguyên giá trị cũ.")

        email = input(f"Nhập email mới (hiện tại: {sv_hien_tai[3]}, bỏ qua nếu không đổi): ") or sv_hien_tai[3]
        dien_thoai = input(f"Nhập số điện thoại mới (hiện tại: {sv_hien_tai[4]}, bỏ qua nếu không đổi): ") or sv_hien_tai[4]
        dia_chi = input(f"Nhập địa chỉ mới (hiện tại: {sv_hien_tai[5]}, bỏ qua nếu không đổi): ") or sv_hien_tai[5]

        query = """
            UPDATE students
            SET ten = %s, ngay_sinh = %s, email = %s, dien_thoai = %s, dia_chi = %s
            WHERE mssv = %s
        """
        params = (ten, ngay_sinh, email, dien_thoai, dia_chi, mssv)
        if self.db_manager.execute_query(query, params) == 1:
            print("Sửa thông tin sinh viên thành công!")
        else:
            print("Sửa thông tin sinh viên thất bại hoặc không có thay đổi.")

    def xoa_sinh_vien(self):
        print("\n--- Xóa Sinh Viên ---")
        mssv = input("Nhập MSSV của sinh viên cần xóa: ")
        
        enrollments = self.db_manager.execute_query("SELECT * FROM enrollment WHERE mssv = %s", (mssv,))
        if enrollments:
            print(f"Lưu ý: Sinh viên {mssv} đang có {len(enrollments)} đăng ký khóa học. "
                  "Các đăng ký này sẽ tự động bị xóa theo (do cài đặt ON DELETE CASCADE).")
            confirm = input("Bạn có chắc chắn muốn xóa sinh viên này và các đăng ký liên quan? (y/n): ").lower()
            if confirm != 'y':
                print("Hủy thao tác xóa.")
                return

        query = "DELETE FROM students WHERE mssv = %s"
        rows_affected = self.db_manager.execute_query(query, (mssv,))
        if rows_affected is not None:
            if rows_affected > 0:
                print(f"Đã xóa sinh viên {mssv} thành công.")
            else:
                print(f"Không tìm thấy sinh viên với MSSV: {mssv}")
        else:
            print("Xóa sinh viên thất bại.")

    def tim_kiem_sinh_vien(self):
        print("\n--- Tìm Kiếm Sinh Viên ---")
        tu_khoa = input("Nhập MSSV hoặc tên sinh viên để tìm kiếm: ")
        query = """
            SELECT mssv, ten, ngay_sinh, email, dien_thoai, dia_chi FROM students
            WHERE mssv LIKE %s OR ten LIKE %s
        """
        params = (f'%{tu_khoa}%', f'%{tu_khoa}%')
        results = self.db_manager.execute_query(query, params)

        if results:
            print("\nKết quả tìm kiếm:")
            for row in results:
                row_list = list(row)
                if isinstance(row_list[2], datetime.date):
                    row_list[2] = row_list[2].strftime("%Y-%m-%d")
                sinh_vien = SinhVien(*row_list)
                print(sinh_vien)
        else:
            print(f"Không tìm thấy sinh viên nào với từ khóa '{tu_khoa}'.")

    def hien_thi_tat_ca_sinh_vien(self):
        print("\n--- Danh Sách Tất Cả Sinh Viên ---")
        query = "SELECT mssv, ten, ngay_sinh, email, dien_thoai, dia_chi FROM students"
        results = self.db_manager.execute_query(query)
        if results:
            for row in results:
                row_list = list(row)
                if isinstance(row_list[2], datetime.date):
                    row_list[2] = row_list[2].strftime("%Y-%m-%d")
                sinh_vien = SinhVien(*row_list)
                print(sinh_vien)
        else:
            print("Chưa có sinh viên nào trong hệ thống.")

    def them_khoa_hoc(self):
        print("\n--- Thêm Khóa Học ---")
        ma_khoa_hoc = input("Nhập mã khóa học: ")
        if self.db_manager.execute_query("SELECT * FROM courses WHERE ma_khoa_hoc = %s", (ma_khoa_hoc,)):
            print(f"Lỗi: Mã khóa học {ma_khoa_hoc} đã tồn tại.")
            return

        ten_khoa_hoc = input("Nhập tên khóa học: ")
        mo_ta = input("Nhập mô tả khóa học: ")
        
        while True:
            try:
                so_tin_chi = int(input("Nhập số tín chỉ: "))
                break
            except ValueError:
                print("Số tín chỉ phải là một số nguyên. Vui lòng nhập lại.")

        khoa_hoc = KhoaHoc(ma_khoa_hoc, ten_khoa_hoc, mo_ta, so_tin_chi)
        query = """
            INSERT INTO courses (ma_khoa_hoc, ten_khoa_hoc, mo_ta, so_tin_chi)
            VALUES (%s, %s, %s, %s)
        """
        params = (khoa_hoc.ma_khoa_hoc, khoa_hoc.ten_khoa_hoc, khoa_hoc.mo_ta, khoa_hoc.so_tin_chi)
        if self.db_manager.execute_query(query, params) == 1:
            print("Thêm khóa học thành công!")
        else:
            print("Thêm khóa học thất bại.")

    def sua_khoa_hoc(self):
        print("\n--- Sửa Thông Tin Khóa Học ---")
        ma_khoa_hoc = input("Nhập mã khóa học cần sửa: ")
        kh_hien_tai = self.db_manager.execute_query("SELECT * FROM courses WHERE ma_khoa_hoc = %s", (ma_khoa_hoc,)).copy()
        if not kh_hien_tai:
            print(f"Không tìm thấy khóa học với mã: {ma_khoa_hoc}")
            return
        
        kh_hien_tai = kh_hien_tai[0]
        print("Thông tin hiện tại:")
        print(f"Mã KH: {kh_hien_tai[0]}, Tên KH: {kh_hien_tai[1]}, MT: {kh_hien_tai[2]}, TC: {kh_hien_tai[3]}")

        ten_khoa_hoc = input(f"Nhập tên khóa học mới (hiện tại: {kh_hien_tai[1]}, bỏ qua nếu không đổi): ") or kh_hien_tai[1]
        mo_ta = input(f"Nhập mô tả mới (hiện tại: {kh_hien_tai[2]}, bỏ qua nếu không đổi): ") or kh_hien_tai[2]
        
        so_tin_chi_input = input(f"Nhập số tín chỉ mới (hiện tại: {kh_hien_tai[3]}, bỏ qua nếu không đổi): ")
        so_tin_chi = kh_hien_tai[3]
        if so_tin_chi_input:
            try:
                so_tin_chi = int(so_tin_chi_input)
            except ValueError:
                print("Số tín chỉ không hợp lệ. Giữ nguyên giá trị cũ.")

        query = """
            UPDATE courses
            SET ten_khoa_hoc = %s, mo_ta = %s, so_tin_chi = %s
            WHERE ma_khoa_hoc = %s
        """
        params = (ten_khoa_hoc, mo_ta, so_tin_chi, ma_khoa_hoc)
        if self.db_manager.execute_query(query, params) == 1:
            print("Sửa thông tin khóa học thành công!")
        else:
            print("Sửa thông tin khóa học thất bại hoặc không có thay đổi.")

    def xoa_khoa_hoc(self):
        print("\n--- Xóa Khóa Học ---")
        ma_khoa_hoc = input("Nhập mã khóa học cần xóa: ")

        enrollments = self.db_manager.execute_query("SELECT * FROM enrollment WHERE ma_khoa_hoc = %s", (ma_khoa_hoc,))
        if enrollments:
            print(f"Lưu ý: Khóa học {ma_khoa_hoc} đang có {len(enrollments)} sinh viên đăng ký. "
                  "Các đăng ký này sẽ tự động bị xóa theo (do cài đặt ON DELETE CASCADE).")
            confirm = input("Bạn có chắc chắn muốn xóa khóa học này và các đăng ký liên quan? (y/n): ").lower()
            if confirm != 'y':
                print("Hủy thao tác xóa.")
                return

        query = "DELETE FROM courses WHERE ma_khoa_hoc = %s"
        rows_affected = self.db_manager.execute_query(query, (ma_khoa_hoc,))
        if rows_affected is not None:
            if rows_affected > 0:
                print(f"Đã xóa khóa học {ma_khoa_hoc} thành công.")
            else:
                print(f"Không tìm thấy khóa học với mã: {ma_khoa_hoc}")
        else:
            print("Xóa khóa học thất bại.")

    def tim_kiem_khoa_hoc(self):
        print("\n--- Tìm Kiếm Khóa Học ---")
        tu_khoa = input("Nhập mã hoặc tên khóa học để tìm kiếm: ")
        query = """
            SELECT ma_khoa_hoc, ten_khoa_hoc, mo_ta, so_tin_chi FROM courses
            WHERE ma_khoa_hoc LIKE %s OR ten_khoa_hoc LIKE %s
        """
        params = (f'%{tu_khoa}%', f'%{tu_khoa}%')
        results = self.db_manager.execute_query(query, params)

        if results:
            print("\nKết quả tìm kiếm:")
            for row in results:
                khoa_hoc = KhoaHoc(*row)
                print(khoa_hoc)
        else:
            print(f"Không tìm thấy khóa học nào với từ khóa '{tu_khoa}'.")

    def hien_thi_tat_ca_khoa_hoc(self):
        print("\n--- Danh Sách Tất Cả Khóa Học ---")
        results = self.db_manager.execute_query("SELECT ma_khoa_hoc, ten_khoa_hoc, mo_ta, so_tin_chi FROM courses")
        if results:
            for row in results:
                khoa_hoc = KhoaHoc(*row)
                print(khoa_hoc)
        else:
            print("Chưa có khóa học nào trong hệ thống.")

    def them_dang_ky(self):
        print("\n--- Thêm Đăng Ký Khóa Học ---")
        ma_dang_ky = input("Nhập mã số đăng ký: ")
        if self.db_manager.execute_query("SELECT * FROM enrollment WHERE ma_dang_ky = %s", (ma_dang_ky,)):
            print(f"Lỗi: Mã đăng ký {ma_dang_ky} đã tồn tại.")
            return

        mssv = input("Nhập MSSV của sinh viên: ")
        ma_khoa_hoc = input("Nhập mã khóa học: ")

        sv_exists = self.db_manager.execute_query("SELECT * FROM students WHERE mssv = %s", (mssv,))
        kh_exists = self.db_manager.execute_query("SELECT * FROM courses WHERE ma_khoa_hoc = %s", (ma_khoa_hoc,))

        if not sv_exists:
            print(f"Lỗi: MSSV {mssv} không tồn tại.")
            return
        if not kh_exists:
            print(f"Lỗi: Mã khóa học {ma_khoa_hoc} không tồn tại.")
            return
        
        existing_enrollment = self.db_manager.execute_query(
            "SELECT * FROM enrollment WHERE mssv = %s AND ma_khoa_hoc = %s", 
            (mssv, ma_khoa_hoc)
        )
        if existing_enrollment:
            print(f"Sinh viên {mssv} đã đăng ký khóa học {ma_khoa_hoc} này rồi.")
            return

        dang_ky = DangKy(ma_dang_ky, mssv, ma_khoa_hoc)
        query = """
            INSERT INTO enrollment (ma_dang_ky, mssv, ma_khoa_hoc, ngay_dang_ky)
            VALUES (%s, %s, %s, %s)
        """
        params = (dang_ky.ma_dang_ky, dang_ky.mssv, dang_ky.ma_khoa_hoc, dang_ky.ngay_dang_ky)
        if self.db_manager.execute_query(query, params) == 1:
            print("Thêm đăng ký khóa học thành công!")
        else:
            print("Thêm đăng ký khóa học thất bại.")

    def xoa_dang_ky(self):
        print("\n--- Xóa Đăng Ký Khóa Học ---")
        ma_dang_ky = input("Nhập mã số đăng ký cần xóa: ")
        
        query = "DELETE FROM enrollment WHERE ma_dang_ky = %s"
        rows_affected = self.db_manager.execute_query(query, (ma_dang_ky,))
        if rows_affected is not None:
            if rows_affected > 0:
                print(f"Đã xóa đăng ký {ma_dang_ky} thành công.")
            else:
                print(f"Không tìm thấy đăng ký với mã: {ma_dang_ky}")
        else:
            print("Xóa đăng ký khóa học thất bại.")

    def tim_kiem_dang_ky(self):
        print("\n--- Tìm Kiếm Đăng Ký Khóa Học ---")
        mssv = input("Nhập MSSV (bỏ qua nếu không tìm theo MSSV): ")
        ma_khoa_hoc = input("Nhập mã khóa học (bỏ qua nếu không tìm theo Mã KH): ")

        query = "SELECT ma_dang_ky, mssv, ma_khoa_hoc, ngay_dang_ky FROM enrollment WHERE 1=1"
        params = []
        if mssv:
            query += " AND mssv LIKE %s"
            params.append(f'%{mssv}%')
        if ma_khoa_hoc:
            query += " AND ma_khoa_hoc LIKE %s"
            params.append(f'%{ma_khoa_hoc}%')
        
        if not mssv and not ma_khoa_hoc:
            print("Vui lòng nhập MSSV hoặc Mã khóa học để tìm kiếm.")
            return

        results = self.db_manager.execute_query(query, tuple(params))

        if results:
            print("\nKết quả tìm kiếm:")
            for row in results:
                row_list = list(row)
                if isinstance(row_list[3], datetime.date):
                    row_list[3] = row_list[3].strftime("%Y-%m-%d")
                dang_ky = DangKy(*row_list)
                print(dang_ky)
        else:
            print("Không tìm thấy đăng ký nào phù hợp.")

    def hien_thi_tat_ca_dang_ky(self):
        print("\n--- Danh Sách Tất Cả Đăng Ký Khóa Học ---")
        query = "SELECT ma_dang_ky, mssv, ma_khoa_hoc, ngay_dang_ky FROM enrollment"
        results = self.db_manager.execute_query(query)
        if results:
            for row in results:
                row_list = list(row)
                if isinstance(row_list[3], datetime.date):
                    row_list[3] = row_list[3].strftime("%Y-%m-%d")
                dang_ky = DangKy(*row_list)
                print(dang_ky)
        else:
            print("Chưa có đăng ký khóa học nào trong hệ thống.")

    def menu_quan_ly_sinh_vien(self):
        while True:
            print("\n===== QUẢN LÝ SINH VIÊN =====")
            print("1. Thêm sinh viên")
            print("2. Sửa thông tin sinh viên")
            print("3. Xóa sinh viên")
            print("4. Tìm kiếm sinh viên")
            print("5. Hiển thị tất cả sinh viên")
            print("0. Quay lại menu chính")
            
            choice = input("Chọn chức năng: ")
            if choice == '1':
                self.them_sinh_vien()
            elif choice == '2':
                self.sua_sinh_vien()
            elif choice == '3':
                self.xoa_sinh_vien()
            elif choice == '4':
                self.tim_kiem_sinh_vien()
            elif choice == '5':
                self.hien_thi_tat_ca_sinh_vien()
            elif choice == '0':
                break
            else:
                print("Lựa chọn không hợp lệ. Vui lòng thử lại.")

    def menu_quan_ly_khoa_hoc(self):
        while True:
            print("\n===== QUẢN LÝ KHÓA HỌC =====")
            print("1. Thêm khóa học")
            print("2. Sửa thông tin khóa học")
            print("3. Xóa khóa học")
            print("4. Tìm kiếm khóa học")
            print("5. Hiển thị tất cả khóa học")
            print("0. Quay lại menu chính")

            choice = input("Chọn chức năng: ")
            if choice == '1':
                self.them_khoa_hoc()
            elif choice == '2':
                self.sua_khoa_hoc()
            elif choice == '3':
                self.xoa_khoa_hoc()
            elif choice == '4':
                self.tim_kiem_khoa_hoc()
            elif choice == '5':
                self.hien_thi_tat_ca_khoa_hoc()
            elif choice == '0':
                break
            else:
                print("Lựa chọn không hợp lệ. Vui lòng thử lại.")

    def menu_quan_ly_dang_ky(self):
        while True:
            print("\n===== QUẢN LÝ ĐĂNG KÝ KHÓA HỌC =====")
            print("1. Thêm đăng ký khóa học")
            print("2. Xóa đăng ký khóa học")
            print("3. Tìm kiếm đăng ký khóa học")
            print("4. Hiển thị tất cả đăng ký")
            print("0. Quay lại menu chính")

            choice = input("Chọn chức năng: ")
            if choice == '1':
                self.them_dang_ky()
            elif choice == '2':
                self.xoa_dang_ky()
            elif choice == '3':
                self.tim_kiem_dang_ky()
            elif choice == '4':
                self.hien_thi_tat_ca_dang_ky()
            elif choice == '0':
                break
            else:
                print("Lựa chọn không hợp lệ. Vui lòng thử lại.")

    def run(self):
        while True:
            print("\n===== HỆ THỐNG QUẢN LÝ ĐĂNG KÝ KHÓA HỌC =====")
            print("1. Quản lý Sinh Viên")
            print("2. Quản lý Khóa Học")
            print("3. Quản lý Đăng Ký Khóa Học")
            print("0. Thoát ứng dụng")

            main_choice = input("Chọn một lựa chọn: ")
            if main_choice == '1':
                self.menu_quan_ly_sinh_vien()
            elif main_choice == '2':
                self.menu_quan_ly_khoa_hoc()
            elif main_choice == '3':
                self.menu_quan_ly_dang_ky()
            elif main_choice == '0':
                print("Đang thoát ứng dụng. Tạm biệt!")
                break
            else:
                print("Lựa chọn không hợp lệ. Vui lòng thử lại.")

if __name__ == "__main__":
    app = QuanLyDangKyKhoaHoc()
    app.run()