using LyricsCounter.Service.Entities.ServiceDefinition;
using System.Threading.Tasks;

namespace LyricsCounter.Service.Api.Client
{
    public interface ILyricsApiClient
    {
        Task<LyricsResult> GetLyrics(string artistName, string songName);
    }
}