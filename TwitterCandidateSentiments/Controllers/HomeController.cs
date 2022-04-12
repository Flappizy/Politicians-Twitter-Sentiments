using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Contracts.IRepository;


namespace TwitterCandidateSentiments.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HomeController : ControllerBase
    {
        private IOpinionsRepo _repo;
        private ICandidates _candidateRepo;

        public HomeController(IOpinionsRepo repo, ICandidates candidateRepo)
        {
            _repo = repo;
            _candidateRepo = candidateRepo;
        }

        [Route("getSentiment")]
        [HttpGet]
        public async Task<IActionResult> GetCandidateSentiment([FromQuery] string candidate)
        {
            //int mydelay = 8000;
            //Thread.Sleep(mydelay);

            if (candidate == null)
            {
                return StatusCode(StatusCodes.Status400BadRequest, new { Error = "Please Search term can not be null" });
            }

            var doesCandidateName = await _candidateRepo.CheckIfCandidateExists(candidate);

            if (doesCandidateName == null)
            {
                return StatusCode(StatusCodes.Status400BadRequest, new { Error = "Candidate Details does not exist within Database" });
            }

            try
            {
                var candidateSentimentDetails = await _repo.GetCandidateSentimentDetailsSoFar(doesCandidateName);
                return Ok(candidateSentimentDetails);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    new { Error = "Sorry Server encountered an error while trying to retrieve candidate's sentiment details" });
            }
        }
    }
}
