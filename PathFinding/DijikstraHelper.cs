//using System.Collections.Generic;
//using System.Linq;

//using Microsoft.Xna.Framework;
//using Utilities.Utility;

//namespace Utilities.PathFinding {
//	static class DijkstraHelper {
//		public static Graph CalculateGraph(Map map, Unit unit, Point position) {

//			Graph graph = new Graph {
//				Source = position.toString(),
//				Vertices = new Dictionary<string, Dictionary<string, int>>()
//			};
			
//			foreach (string vertex in map.navigationGraph.Vertices.Keys.ToList()) {
//				graph.Vertices.Add(vertex, new Dictionary<string, int>());
//				foreach (string neighbor in map.navigationGraph.Vertices[vertex].Keys.ToList()) {
//					Point point = neighbor.Parse();
//					MapCell mapcell = map[point];
//					int cost = Unit.GetTravelCost(unit.UnitType, map[point].terrain);

//					if (cost < int.MaxValue) {
//						graph.Vertices[vertex].Add(neighbor, cost);
//					}
//				}
//			}
			
//			graph.Dijkstra(position.toString(), int.MaxValue);

//			return graph;
//		}

//		public static List<Point> FindRange(Graph graph) {
//			var range = graph.FindReachableVertex();

//			var result = new List<Point>();
//			foreach (string dest in range) {
//				result.Add(dest.Parse());
//			}
//			return result;
//		}

//		public static List<Point> FindPath(Graph graph, Point destination) {
//			var path = graph.FindShortestPath(destination.toString());

//			var result = new List<Point>();
//			foreach (string node in path) {
//				result.Add(node.Parse());
//			}

//			return result;
//		}
//	}
//}
