using FluentAssertions;
using System;
using System.Collections.Generic;
using System.Text;
using TechTalk.SpecFlow;

namespace RoboGuiCopy.Tests
{
    [Binding]
    public class Steps
    {
        private readonly ScenarioContext context;

        public Steps(ScenarioContext context)
        {
            this.context = context;
        }

        [Given(@"a user enters (.*) and (.*)")]
        public void GivenAUserEntersAnd(int num1, int num2)
        {
            context.Add("num1", num1);
            context.Add("num2", num2);
        }

        [When(@"the user clicks add")]
        public void WhenTheUserClicksAdd()
        {
            var num1 = context.Get<int>("num1");
            var num2 = context.Get<int>("num2");
            context.Add("answer", num1 + num2);
        }

        [Then(@"the answer is (.*)")]
        public void ThenTheAnswerIs(int expected)
        {
            var actual = context.Get<int>("answer");
            actual.Should().Be(expected);
        }


    }
}
