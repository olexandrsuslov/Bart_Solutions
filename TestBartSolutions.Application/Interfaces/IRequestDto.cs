using TestBartSolutions.Core.Models;
using TestBartSolutions.Models;

namespace TestBartSolutions.Application.Interfaces;

public interface IRequestDto
{
    public Task PostDto(RequestDto requestDto);
}