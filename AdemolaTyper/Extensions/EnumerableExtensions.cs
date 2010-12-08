using System;
using System.Collections.Generic;

namespace AdemolaTyper.Extensions
{
    public static class EnumerableExtensions
    {
        public static void each<T>(this IEnumerable<T> items, Action<T> action)
        {
            foreach (var item in items) action(item);
        }

        public static IEnumerable<T> one_at_a_time<T>(this IEnumerable<T> items)
        {
            foreach (var item in items) yield return item;
        }
        public static IEnumerable<Output> map_all_using<Input, Output>(this IEnumerable<Input> items_to_map, IMapper<Input, Output> mapper)
        {
            foreach (var item in items_to_map) yield return mapper.map_from(item);
        }

        public static void visit_all_items_with<T>(this IEnumerable<T> items, IVisitor<T> visitor)
        {
            foreach (var t in items) visitor.visit(t);
        }

        public static Result get_result_of_visiting_all_items_with<T, Result>(this IEnumerable<T> items, IValueReturningVisitor<T, Result> visitor)
        {
            visit_all_items_with(items, visitor);
            return visitor.GetResult();
        }


        public static void for_each(this string[] source, Action<string> action)
        {
            foreach (string item in source)
            {
                action(item);
            }
        }
    }
}