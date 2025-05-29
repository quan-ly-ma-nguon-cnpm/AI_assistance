using Microsoft.AspNetCore.Mvc;
using SEHannah.Models;
using System.Text.Json;
using System.Text;

public class GiangVienController : Controller
{
    private readonly HttpClient _httpClient;

    public GiangVienController(IHttpClientFactory factory)
    {
        _httpClient = factory.CreateClient();
    }

    public async Task<IActionResult> DuyetPhanHoi()
    {
        var res = await _httpClient.GetAsync("http://localhost:8000/api/phan-hoi");
        if (!res.IsSuccessStatusCode) return View(new List<PhanHoiViewModel>());
        
        var body = await res.Content.ReadAsStringAsync();
        var data = JsonSerializer.Deserialize<List<PhanHoiViewModel>>(body);
        return View(data);
    }

    [HttpPost]
    public async Task<IActionResult> DuyetPhanHoi(int id)
    {
        await _httpClient.PostAsync($"http://localhost:8000/api/phan-hoi/duyet/{id}", null);
        return RedirectToAction("DuyetPhanHoi");
    }

    public IActionResult CapNhatKienThuc()
    {
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> CapNhatKienThuc(KienThucViewModel model)
    {
        var json = JsonSerializer.Serialize(model);
        var content = new StringContent(json, Encoding.UTF8, "application/json");
        var res = await _httpClient.PostAsync("http://localhost:8000/api/kien-thuc", content);

        if (res.IsSuccessStatusCode)
            ViewBag.ThongBao = "Cập nhật thành công!";
        else
            ViewBag.ThongBao = "Có lỗi xảy ra.";

        return View();
    }

    public IActionResult DashBoard()
    {
        return View(); // Trang hiển thị thống kê sau này
    }
}
