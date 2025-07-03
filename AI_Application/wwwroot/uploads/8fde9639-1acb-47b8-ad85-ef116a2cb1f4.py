import numpy as np
import matplotlib.pyplot as plt

# Generate sample data
np.random.seed(42)
X = np.random.randn(100, 2) * 0.5
X = np.r_[X, X + [2, 2], X + [-2, 2]]

plt.scatter(X[:, 0], X[:, 1], alpha=0.7)
plt.title("Sample Data for DBSCAN")
plt.show()

def plot_circles(X, eps):
    for point in X:
        circle = plt.Circle(point, eps, fill=False, linestyle='--')
        plt.gca().add_artist(circle)

eps = 0.5
min_pts = 5

plt.figure(figsize=(10, 5))
plt.subplot(121)
plt.scatter(X[:, 0], X[:, 1], alpha=0.7)
plot_circles(X[:5], eps)
plt.title(f"Epsilon Neighborhoods (eps={eps})")

plt.subplot(122)
plt.scatter(X[:, 0], X[:, 1], alpha=0.7)
plt.scatter(X[0], X[1], s=100, c='red')
plt.title(f"Core Point (min_pts={min_pts})")
plt.show()

def euclidean_distance(point1, point2):
    return np.sqrt(np.sum((point1 - point2) ** 2))

def get_neighbors(X, point_idx, eps):
    distances = [euclidean_distance(X[point_idx], other_point) for other_point in X]
    return [i for i, dist in enumerate(distances) if dist <= eps]

point_idx = 0
neighbors = get_neighbors(X, point_idx, eps)
print(f"Number of neighbors for point {point_idx}: {len(neighbors)}")

def find_core_points(X, eps, min_pts):
    core_points = []
    for i in range(len(X)):
        if len(get_neighbors(X, i, eps)) >= min_pts:
            core_points.append(i)
    return core_points

core_points = find_core_points(X, eps, min_pts)
print(f"Number of core points: {len(core_points)}")

plt.scatter(X[:, 0], X[:, 1], alpha=0.7)
plt.scatter(X[core_points, 0], X[core_points, 1], c='red', s=50)
plt.title("Core Points Identified")
plt.show()

def expand_cluster(X, labels, point_idx, neighbors, cluster_id, eps, min_pts):
    labels[point_idx] = cluster_id
    i = 0
    while i < len(neighbors):
        neighbor = neighbors[i]
        if labels[neighbor] == -1:  # Noise becomes border point
            labels[neighbor] = cluster_id
        elif labels[neighbor] == 0:  # Unvisited
            labels[neighbor] = cluster_id
            new_neighbors = get_neighbors(X, neighbor, eps)
            if len(new_neighbors) >= min_pts:
                neighbors.extend(new_neighbors)
        i += 1
    return labels

# This function will be used in the main DBSCAN algorithm
def dbscan(X, eps, min_pts):
    labels = [0] * len(X)  # 0: unvisited, -1: noise
    cluster_id = 0
    core_points = find_core_points(X, eps, min_pts)
    
    for point_idx in range(len(X)):
        if labels[point_idx] != 0:
            continue
        if point_idx in core_points:
            cluster_id += 1
            neighbors = get_neighbors(X, point_idx, eps)
            labels = expand_cluster(X, labels, point_idx, neighbors, cluster_id, eps, min_pts)
        else:
            labels[point_idx] = -1  # Noise
    return labels

# Run DBSCAN
labels = dbscan(X, eps, min_pts)

unique_labels = set(labels)
colors = plt.cm.rainbow(np.linspace(0, 1, len(unique_labels)))

plt.figure(figsize=(10, 7))
for label, color in zip(unique_labels, colors):
    if label == -1:
        color = 'gray'  # Use gray for noise points
    class_member_mask = (np.array(labels) == label)
    xy = X[class_member_mask]
    plt.scatter(xy[:, 0], xy[:, 1], c=[color], alpha=0.7, label=f'Cluster {label}')

plt.title("DBSCAN Clustering Results")
plt.legend()
plt.show()

from sklearn.datasets import make_moons

# Generate a more complex dataset
X_moons, _ = make_moons(n_samples=200, noise=0.05, random_state=42)

# Run DBSCAN with adjusted parameters
eps_moons = 0.2
min_pts_moons = 5
labels_moons = dbscan(X_moons, eps_moons, min_pts_moons)

# Visualize results
plt.figure(figsize=(10, 7))
unique_labels = set(labels_moons)
colors = plt.cm.rainbow(np.linspace(0, 1, len(unique_labels)))

for label, color in zip(unique_labels, colors):
    if label == -1:
        color = 'gray'
    class_member_mask = (np.array(labels_moons) == label)
    xy = X_moons[class_member_mask]
    plt.scatter(xy[:, 0], xy[:, 1], c=[color], alpha=0.7, label=f'Cluster {label}')

plt.title("DBSCAN on Complex Dataset")
plt.legend()
plt.show()

def plot_dbscan_results(X, eps, min_pts):
    labels = dbscan(X, eps, min_pts)
    unique_labels = set(labels)
    colors = plt.cm.rainbow(np.linspace(0, 1, len(unique_labels)))
    
    for label, color in zip(unique_labels, colors):
        if label == -1:
            color = 'gray'
        class_member_mask = (np.array(labels) == label)
        xy = X[class_member_mask]
        plt.scatter(xy[:, 0], xy[:, 1], c=[color], alpha=0.7)
    plt.title(f"DBSCAN: eps={eps}, min_pts={min_pts}")

plt.figure(figsize=(15, 5))
eps_values = [0.1, 0.3, 0.5]

for i, eps in enumerate(eps_values):
    plt.subplot(1, 3, i+1)
    plot_dbscan_results(X_moons, eps, min_pts_moons)

plt.tight_layout()
plt.show()

# Sample city data (longitude, latitude)
cities = np.array([
    [-122.4194, 37.7749],  # San Francisco
    [-122.2711, 37.8044],  # Berkeley
    [-122.0839, 37.3861],  # San Jose
    [-118.2437, 34.0522],  # Los Angeles
    [-117.1611, 32.7157],  # San Diego
    [-74.0060, 40.7128],   # New York City
    [-73.9442, 40.6782],   # Brooklyn
    [-73.7845, 40.9115],   # White Plains
    [-87.6298, 41.8781],   # Chicago
    [-87.9065, 41.9742],   # O'Hare Airport
])

# Run DBSCAN
eps_cities = 1  # Approximately 111 km
min_pts_cities = 2
labels_cities = dbscan(cities, eps_cities, min_pts_cities)

# Visualize results
plt.figure(figsize=(12, 8))
scatter = plt.scatter(cities[:, 0], cities[:, 1], c=labels_cities, cmap='viridis')
plt.colorbar(scatter)
plt.title("City Clusters based on Geographical Proximity")
plt.xlabel("Longitude")
plt.ylabel("Latitude")
plt.show()