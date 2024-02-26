using System;
using System.Collections.Generic;
using Xunit;

namespace UnitTests
{
    internal class OperationResultTests
    {
        [Fact]
        public void Success_Result_Should_Have_No_Errors_And_Have_Payload()
        {
           
            int expectedPayload = 42;

            var result = Result<int>.Success(expectedPayload);

            Assert.True(result.IsSuccess);
            Assert.Empty(result.Errors);
            Assert.Equal(expectedPayload, result.Payload);
        }

        [Fact]
        public void Failure_Result_Should_Have_Errors_And_No_Payload()
        {
            var expectedError = new Error(1001, "Test error");

            var result = Result<string>.Failure(expectedError);

            Assert.False(result.IsSuccess);
            Assert.Single(result.Errors);
            Assert.Equal(expectedError, result.Errors[0]);
            Assert.Null(result.Payload);
        }

        [Fact]
        public void Combine_Should_Return_Combined_Result_With_No_Errors_If_All_Successful()
        {
            var result1 = Result<int>.Success(1);
            var result2 = Result<int>.Success(2);
            var result3 = Result<int>.Success(3);

            var combinedResult = Result<IEnumerable<int>>.Combine(result1, result2, result3);

            Assert.True(combinedResult.IsSuccess);
            Assert.Empty(combinedResult.Errors);
            Assert.Equal(new[] { 1, 2, 3 }, combinedResult.Payload);
        }

        [Fact]
        public void Combine_Should_Return_Combined_Result_With_Errors_If_Any_Failure()
        {
            var result1 = Result<int>.Success(1);
            var result2 = Result<int>.Failure(new Error(1001, "Error 1"));
            var result3 = Result<int>.Success(3);

            var combinedResult = Result<IEnumerable<int>>.Combine(result1, result2, result3);

            Assert.False(combinedResult.IsSuccess);
            Assert.Single(combinedResult.Errors);
            Assert.Equal(1001, combinedResult.Errors[0].Code);
            Assert.Equal("Error 1", combinedResult.Errors[0].Message);
            Assert.Null(combinedResult.Payload);
        }

        [Fact]
        public void Implicit_Conversion_Should_Unwrap_Successful_Result()
        {
            var expectedResult = 42;
            var result = Result<int>.Success(expectedResult);

            int unwrappedValue = result;

            Assert.Equal(expectedResult, unwrappedValue);
        }

        [Fact]
        public void Implicit_Conversion_Should_Throw_Exception_For_Failure_Result()
        {
  
            var result = Result<string>.Failure(new Error(1001, "Test error"));

            Assert.Throws<InvalidOperationException>(() =>
            {
                string unwrappedValue = result;
            });
        }
    }
}
