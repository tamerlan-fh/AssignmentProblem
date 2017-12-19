using AssignmentProblem.Library;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Dynamic;
using AssignmentProblem.ViewModels;

namespace AssignmentProblem.Managers
{
    /// <summary>
    /// менеджер назначения
    /// </summary>
    class AssignmentManager
    {
        public static AssignmentManager Instance
        {
            get
            {
                if(instance == null)
                    instance = new AssignmentManager();
                return instance;
            }
        }
        private static AssignmentManager instance;
        private AssignmentManager() { }


        //public Dictionary<Agent, List<Operation>> DoAssignment(Agent[] agents, Operation[] operations)
        //{
        //    var assignment = new Dictionary<Agent, List<Operation>>();
        //    if(!agents.Any()) return assignment;

        //    foreach(var agent in agents)
        //        assignment.Add(agent, new List<Operation>());

        //    if(!operations.Any()) return assignment;

        //    var cicle = operations.Length / agents.Length;

        //    for(int i = 0; i < cicle; i++)
        //        for(int j = 0; j < agents.Length; j++)
        //            assignment[agents[j]].Add(operations[i * agents.Length + j]);

        //    var mod = operations.Length % agents.Length;
        //    for(int i = 0; i < mod; i++)
        //        assignment[agents[i]].Add(operations[cicle * agents.Length + i]);

        //    return assignment;
        //}

        //public static int[] KuhnMunkres(int[,] a)
        //{
        //    int N = a.GetLength(0);
        //    if(N == 0)
        //        return new int[0];

        //    int[] lx = new int[N], ly = new int[N];   // функция маркировки для вершин в первом и втором разделах
        //    int[] mx = new int[N], my = new int[N];   // mx[u]=v, my[v]=u <==> u and v are currently matched;  -1 values means 'not matched'
        //    int[] px = new int[N], py = new int[N];   // predecessor arrays.  used in DFS to reconstruct paths.
        //    int[] stack = new int[N];


        //    for(int i = 0; i < N; i++)
        //    {
        //        lx[i] = a[i, 0];
        //        for(int j = 0; j < N; j++)
        //            if(a[i, j] > lx[i]) lx[i] = a[i, j];
        //        ly[i] = 0;

        //        mx[i] = my[i] = -1;
        //    }

        //    for(int size = 0; size < N;)
        //    {
        //        int s;
        //        for(s = 0; mx[s] != -1; s++) ;

        //        // s is an unmatched vertex in the first partition.
        //        // At the current iteration we will either find an augmenting path
        //        // starting at s, or we'll extend the equality subgraph so that
        //        // such a path will exist at the next iteration.

        //        for(int i = 0; i < N; i++)
        //            px[i] = py[i] = -1;
        //        px[s] = s;

        //        // DFS
        //        int t = -1;
        //        stack[0] = s;
        //        for(int top = 1; top > 0;)
        //        {
        //            int u = stack[--top];
        //            for(int v = 0; v < N; v++)
        //            {
        //                if(lx[u] + ly[v] == a[u, v] && py[v] == -1)
        //                {
        //                    if(my[v] == -1)
        //                    {
        //                        // we've found an augmenting path
        //                        t = v;
        //                        py[t] = u;
        //                        top = 0;
        //                        break;
        //                    }

        //                    py[v] = u;
        //                    px[my[v]] = v;
        //                    stack[top++] = my[v];
        //                }
        //            }
        //        }

        //        if(t != -1)
        //        {
        //            // augment along the found path
        //            while(true)
        //            {
        //                int u = py[t];
        //                mx[u] = t;
        //                my[t] = u;
        //                if(u == s) break;
        //                t = px[u];
        //            }
        //            ++size;
        //        }
        //        else
        //        {
        //            // No augmenting path exists from s in the current equality graph,
        //            // Modify labelling function a bit...

        //            int delta = int.MaxValue;
        //            for(int u = 0; u < N; u++)
        //            {
        //                if(px[u] == -1) continue;
        //                for(int v = 0; v < N; v++)
        //                {
        //                    if(py[v] != -1) continue;
        //                    int z = lx[u] + ly[v] - a[u, v];
        //                    if(z < delta)
        //                        delta = z;
        //                }
        //            }

        //            for(int i = 0; i < N; i++)
        //            {
        //                if(px[i] != -1) lx[i] -= delta;
        //                if(py[i] != -1) ly[i] += delta;
        //            }
        //        }
        //    }

        //    // Verify optimality
        //    bool correct = true;
        //    for(int u = 0; u < N; u++)
        //    {
        //        for(int v = 0; v < N; v++)
        //        {
        //            correct &= (lx[u] + ly[v] >= a[u, v]);
        //            if(mx[u] == v)
        //                correct &= (lx[u] + ly[v] == a[u, v]);

        //            if(!correct)
        //            {
        //                throw new Exception(
        //                    "*** Internal error: optimality conditions are not satisfied ***\n" +
        //                    "Most probably an overflow occurred");
        //            }
        //        }
        //    }

        //    return mx;
        //}

        public Dictionary<AgentViewModel, List<OperationViewModel>> DoAssignment(AgentViewModel[] agents, OperationViewModel[] operations)
        {
            var assignment = new Dictionary<AgentViewModel, List<OperationViewModel>>();
            foreach(var agent in agents)
                assignment.Add(agent, new List<OperationViewModel>());

            int height = agents.Length;
            int width = operations.Length;

            int rang = Math.Max(height, width);

            var matrix = new long[rang, rang];

            for(int i = 0; i < height; i++)
                for(int j = 0; j < width; j++)
                    matrix[i, j] = (agents[i].OperationsTime + Operation.GetTiming(agents[i].CpuFrequency, operations[j].Complexity)).Ticks;

            if(height < rang)
                for(int i = height; i < rang; i++)
                    for(int j = 0; j < rang; j++)
                        matrix[i, j] = TimeSpan.MaxValue.Ticks;

            if(width < rang)
                for(int i = 0; i < rang; i++)
                    for(int j = width; j < rang; j++)
                        matrix[i, j] = TimeSpan.MaxValue.Ticks;


            try
            {

                // Значения, вычитаемые из строк (u) и столбцов (v)
                // VInt u(height, 0), v(width, 0);
                var u = new long[rang];
                var v = new long[rang];

                // Индекс помеченной клетки в каждом столбце
                var marks = new int[rang];
                for(int i = 0; i < rang; i++)
                    marks[i] = -1;

                // Будем добавлять строки матрицы одну за другой
                int count = 0;
                for(int i = 0; i < rang; i++)
                {
                    var links = new List<int>();
                    var mins = new List<long>();
                    var visited = new List<long>();

                    for(int a = 0; a < rang; a++)
                    {
                        links.Add(-1);
                        mins.Add(long.MaxValue);
                        visited.Add(0);
                    }

                    // Разрешение коллизий (создание "чередующейся цепочки" из нулевых элементов)
                    long markedI = i;
                    int markedJ = -1;
                    int j = 0;
                    while(markedI != -1)
                    {
                        // Обновим информацию о минимумах в посещенных строках непосещенных столбцов
                        // Заодно поместим в j индекс непосещенного столбца с самым маленьким из них
                        j = -1;
                        for(int j1 = 0; j1 < rang; j1++)
                            if(visited[j1] != 1)
                            {
                                if(matrix[markedI, j1] - u[markedI] - v[j1] < mins[j1])
                                {
                                    mins[j1] = matrix[markedI, j1] - u[markedI] - v[j1];
                                    links[j1] = markedJ;
                                }
                                if(j == -1 || mins[j1] < mins[j])
                                    j = j1;
                            }

                        // Теперь нас интересует элемент с индексами (markIndices[links[j]], j)
                        // Произведем манипуляции со строками и столбцами так, чтобы он обнулился
                        var delta = mins[j];
                        for(int j1 = 0; j1 < rang; j1++)
                            if(visited[j1] == 1)
                            {
                                u[marks[j1]] += delta;
                                v[j1] -= delta;
                            }
                            else
                            {
                                mins[j1] -= delta;
                            }
                        u[i] += delta;

                        // Если коллизия не разрешена - перейдем к следующей итерации
                        visited[j] = 1;
                        markedJ = j;
                        markedI = marks[j];
                        count++;
                    }

                    // Пройдем по найденной чередующейся цепочке клеток, снимем отметки с
                    // отмеченных клеток и поставим отметки на неотмеченные
                    for(; links[j] != -1; j = links[j])
                        marks[j] = marks[links[j]];
                    marks[j] = i;
                }

                for(int j = 0; j < width; j++)
                {
                    if(marks[j] == -1 || marks[j] >= height) continue;

                    assignment[agents[marks[j]]].Add(operations[j]);
                }

                //// Вернем результат в естественной форме
                //List<List<int>> result = new List<List<int>>();
                //for(int j = 0; j < rang; j++)
                //    if(marks[j] != -1)
                //    {
                //        dynamic item = new ExpandoObject();
                //        item.operation1 = operations[j];
                //        item.agent1 = agents[marks[j]];
                //        item.time1 = Operation.GetTiming(operations[j].Complexity, agents[marks[j]].CpuFrequency);

                //        item.operation2 = operations[marks[j]];
                //        item.agent2 = agents[j];
                //        item.time2 = Operation.GetTiming(operations[marks[j]].Complexity, agents[j].CpuFrequency);
                //        items[j] = item;
                //        result.Add(new List<int>() { marks[j], j });
                //    }

                return assignment;
            }
            catch(Exception ex)
            {
                return assignment;
            }
        }
    }
}
