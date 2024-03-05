using DCA_VEA.Core.Tools.OperationResult;

namespace xUnitTests.Common.OperationResult
{
    public class OperationResultTests
    {
        [Fact]
        public void Success_ShouldReturnSuccessResult()
        {
            // Arrange
            var testPayload = "Test Payload";

            // Act
            var result = Result<string>.Success(testPayload);

            // Assert
            Assert.True(result.IsSuccess);
            Assert.Equal(testPayload, result.Payload);
        }

        [Fact]
        public void Failure_ShouldReturnFailureResultWithSingleError()
        {
            // Arrange
            var errorCode = ErrorCodes.GeneralError;
            var errorMessage = "A general error has occurred.";

            // Act
            var result = Result<string>.Failure(new Error(errorCode, errorMessage));

            // Assert
            Assert.False(result.IsSuccess);
            Assert.Contains(result.Errors, e => e.Code == errorCode && e.Message == errorMessage);
        }

        [Fact]
        public void Failure_ShouldReturnFailureResultWithMultipleErrors()
        {
            // Arrange
            var errors = new List<Error>
            {
                new Error(ErrorCodes.GeneralError, "A general error has occurred."),
                new Error(ErrorCodes.SpecificError, "A specific error has occurred.")
            };

            // Act
            var result = Result<string>.Failure(errors);

            // Assert
            Assert.False(result.IsSuccess);
            Assert.Equal(2, result.Errors.Count);
        }

        [Fact]
        public void ImplicitOperator_ToResultIEnumerable_ShouldConvertSuccessfully()
        {
            // Arrange
            var testPayload = "Test Payload";
            var result = Result<string>.Success(testPayload);

            // Act
            Result<IEnumerable<string>> convertedResult = result;

            // Assert
            Assert.True(convertedResult.IsSuccess);
            Assert.Single(convertedResult.Payload);
            Assert.Contains(testPayload, convertedResult.Payload);
        }

        [Fact]
        public void ImplicitOperator_ToTPayload_ShouldUnwrapPayload()
        {
            // Arrange
            var testPayload = "Test Payload";
            var result = Result<string>.Success(testPayload);

            // Act & Assert
            string unwrappedPayload = result; // This line implicitly tests the conversion
            Assert.Equal(testPayload, unwrappedPayload);
        }

        [Fact]
        public void ImplicitOperator_ToTPayload_ShouldThrowWhenUnwrappingFailedResult()
        {
            // Arrange
            var result = Result<string>.Failure(new Error(ErrorCodes.GeneralError, "A general error has occurred."));

            // Act & Assert
            Assert.Throws<InvalidOperationException>(() => (string)result);
        }
    }
}
