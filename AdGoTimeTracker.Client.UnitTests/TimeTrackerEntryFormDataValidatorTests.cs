using AdGoTimeTracker.Client.Models;
using AdGoTimeTracker.Client.Validators;
using FluentAssertions;
using FluentValidation.TestHelper;

namespace AdGoTimeTracker.Client.UnitTests;

public class TimeTrackerEntryFormDataValidatorTests
{
    private readonly TimeTrackerEntryFormDataValidator _sut = new();

    [Fact]    
    public void Validate_ReturnsError_WhenNoDescriptionIdProvided()
    {
        var testData = new TimeTrackerEntryFormData
        {
            StartTime = new TimeSpan(9, 0, 0),
            EndTime = new TimeSpan(12, 0, 0),
        };
        var result = _sut.TestValidate(testData);
        result.IsValid.Should().BeFalse();
        result.Errors.Should().HaveCount(1);
        result.Errors.Single().PropertyName.Should().Contain(nameof(TimeTrackerEntryFormData.Description));
    }

    [Theory]    
    [InlineData(2)]
    [InlineData(256)]
    public void Validate_ReturnsError_WhenDescriptionLengthIsInvalid(int lengthToTest)
    {        
        var testData = new TimeTrackerEntryFormData
        {
            Description = new string('x', lengthToTest),
            StartTime = new TimeSpan(9, 0, 0),
            EndTime = new TimeSpan(12, 0, 0),
        };
        var result = _sut.TestValidate(testData);
        result.IsValid.Should().BeFalse();
        result.Errors.Should().HaveCount(1);
        result.Errors.Single().PropertyName.Should().Contain(nameof(TimeTrackerEntryFormData.Description));
    }

    [Fact]
    public void Validate_ReturnsError_WhenStartAfterEnd()
    {
        var testData = new TimeTrackerEntryFormData
        {
            Description = "Hello world",            
            StartTime = new TimeSpan(13,0 , 0),
            EndTime = new TimeSpan(12, 0, 0),
        };
        var result = _sut.TestValidate(testData);
        result.IsValid.Should().BeFalse();
        result.Errors.Should().HaveCount(2);
        result.Errors.First().PropertyName.Should().Contain(nameof(TimeTrackerEntryFormData.StartTime));
        result.Errors.Last().PropertyName.Should().Contain(nameof(TimeTrackerEntryFormData.EndTime));
    }    

    [Fact]
    public void Validate_ReturnsOk_WhenEntryIsGood()
    {
        var testData = new TimeTrackerEntryFormData
        {
            Description = "Hello world",
            StartTime = new TimeSpan(9, 0, 0),
            EndTime = new TimeSpan(12, 0, 0),
        };
        var result = _sut.TestValidate(testData);
        result.IsValid.Should().BeTrue();
        result.Errors.Should().HaveCount(0);        
    }
}