using System.Reflection;
using Xunit.Sdk;

namespace Compressors.Tests
{
    public class RepeatAttribute : DataAttribute
    {
        protected readonly int _count;

        public RepeatAttribute(int count)
        {
            if (count < 1)
            {
                throw new ArgumentOutOfRangeException(nameof(count),
                      "Repeat count must be greater than 0.");
            }

            _count = count;
        }

        public override IEnumerable<object[]> GetData(MethodInfo testMethod)
        {
            foreach (int i in Enumerable.Range(1, _count))
            {
                yield return BuildParamArray(i);
            }
        }

        protected virtual object[] BuildParamArray(int i)
        {
            return [i];
        }
    }

    public class RepeatDataAttribute(int count, params object[]? data) : RepeatAttribute(count)
    {
        protected readonly List<object>? _data = [.. data];

        protected override object[] BuildParamArray(int i)
        {
            if (data is null)
                return [];
            else
                return _data!.Prepend(i).ToArray();
        }
    }
}