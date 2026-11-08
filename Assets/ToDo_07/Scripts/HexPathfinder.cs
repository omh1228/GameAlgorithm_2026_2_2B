using System.Collections.Generic;
using System.Linq;
using AlgoLesson7;

namespace AlgoLesson7.Student
{
    /// <summary>
    /// 목표 유닛 옆의 빈 칸까지 BFS로 이동할 첫 번째 육각칸을 찾습니다.
    /// TODO가 비어 있는 동안에는 단순히 목표와 가까워지는 인접 칸을 선택합니다.
    /// </summary>
    public sealed class HexPathfinder : IHexPathfinder
    {
        public AutoChessBoardCell FindNextStep(
            AutoChessBoardCell start,
            AutoChessBoardCell target,
            IReadOnlyList<AutoChessBoardCell> cells)
        {
            if (start == null || target == null || cells == null)
                return null;

            // TODO 16: target과 거리가 1인 빈 칸들을 goals에 저장하세요.
            HashSet<AutoChessBoardCell> goals = cells.Where(cell => cell.Occupant == null && HexGridUtility.GetDistance(cell, target) == 1).ToHashSet();

            // TODO 17: BFS에 사용할 Queue frontier와
            //          Dictionary<AutoChessBoardCell, AutoChessBoardCell> cameFrom을 만드세요.
            Queue<AutoChessBoardCell> frontier = new Queue<AutoChessBoardCell>();
            Dictionary<AutoChessBoardCell, AutoChessBoardCell> cameFrom = new Dictionary<AutoChessBoardCell, AutoChessBoardCell>();

            // TODO 18: start를 frontier에 넣고 cameFrom에 방문 표시하세요.
            frontier.Enqueue(start);
            cameFrom[start] = null;
            AutoChessBoardCell reachedGoal = null;

            // TODO 19: Queue가 빌 때까지 인접한 빈 칸을 탐색하고
            //          목표 칸에 도착하면 탐색을 종료하세요.
            while (frontier.Count > 0 && reachedGoal == null)
            {
                AutoChessBoardCell current = frontier.Dequeue();

                foreach (AutoChessBoardCell neighbor in HexGridUtility.GetOpenNeighbors(current, cells))
                {
                    if (cameFrom.ContainsKey(neighbor))
                        continue;

                    cameFrom[neighbor] = current;

                    if (goals.Contains(neighbor))
                    {
                        reachedGoal = neighbor;
                        break;
                    }

                    frontier.Enqueue(neighbor);
                }
            }

            // TODO 20: cameFrom을 거꾸로 따라가 start 다음의 첫 번째 칸을 반환하세요.
            if (reachedGoal == null)
                return null;

            AutoChessBoardCell step = reachedGoal;

            while (cameFrom[step] != start)
                step = cameFrom[step];

            return step;
        }

        private static AutoChessBoardCell FindGreedyStep(
            AutoChessBoardCell start,
            AutoChessBoardCell target,
            IReadOnlyList<AutoChessBoardCell> cells)
        {
            AutoChessBoardCell best = null;
            int bestDistance = HexGridUtility.GetDistance(start, target);

            foreach (AutoChessBoardCell neighbor in HexGridUtility.GetOpenNeighbors(start, cells))
            {
                int distance = HexGridUtility.GetDistance(neighbor, target);

                if (distance < bestDistance)
                {
                    best = neighbor;
                    bestDistance = distance;
                }
            }

            return best;
        }
    }
}