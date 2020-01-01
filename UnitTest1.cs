using System;
using System.Linq;
using Akka.Streams;
using Akka.Streams.Dsl;
using Akka.TestKit.Xunit2;
using FluentAssertions;
using FluentAssertions.Extensions;
using Xunit;

namespace KATA_Akka.Streams.Test
{
    public class UnitTest1 : TestKit
    {
        [Fact]
        public void Test1()
        {
            var sinkUnderTest = Flow.Create<int>()
                .Select(x => x * 2)
                .ToMaterialized(Sink.Aggregate<int, int>(0, (sum, i) => sum + i), Keep.Right);

            var task = Source.From(Enumerable.Range(1, 4)).RunWith(sinkUnderTest, Sys.Materializer());

            task.Wait(500.Milliseconds()).Should().BeTrue();
            task.Result.Should().Be(20);
        }

        [Fact]
        public void Test2()
        {

        }
    }
}
