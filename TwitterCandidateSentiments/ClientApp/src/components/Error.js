import PropTypes from 'prop-types';
import './Home.css';
import BarChart from './BarChart';

//This is the component the higher order component will render, if there are no errors
const RenderCandidateSentiment = ({ candidateSentimentDTO }) => {
    return (
        <div>
            <div className="row">
                <div className="col-sm-6 mb-3">
                    <div className="card shadow">
                        <div className="card-body text-center">
                            <h5 className="card-header header-bg-color mb-3">Candidate's Image</h5>
                            <img className="img-size rounded-circle mx-auto img-thumbnail" src={candidateSentimentDTO.candidateBase64Pic} alt={candidateSentimentDTO.candidateName} />
                        </div>
                    </div>
                </div>
                <div className="col-sm-6 mb-3">
                    <div className="card shadow">
                        <div className="card-body text-center">
                            <h5 className="card-header header-bg-color mb-5">Candidate's Name</h5>
                            <p className="card-text display-3">{candidateSentimentDTO.candidateName}</p>
                        </div>
                    </div>
                </div>
            </div>

            <div className="row">
                <div className="col-sm-6 mb-3">
                    <div className="card shadow">
                        <div className="card-body text-center">
                            <h5 className="card-header header-bg-color mb-3">Candidate Sentiment on Twitter</h5>
                            <div className={candidateSentimentDTO.candidateTheme}>
                                <p className="card-text display-3">{candidateSentimentDTO.overAllPublicSentimentOfCandidate}</p>
                            </div>
                        </div>
                    </div>
                </div>
                <div className="col-sm-6 mb-3">
                    <div className="card shadow">
                        <div className="card-body text-center">
                            <h5 className="card-header header-bg-color mb-3">Number of Processed Tweets for candidate</h5>
                            <p className="card-text display-3">{candidateSentimentDTO.numberOfTweetsAssesed}</p>
                        </div>
                    </div>
                </div>
            </div>

            <div class="row g-0 over">
                <div class="col-md-8">
                    <div class="card mb-3 shadow">
                            <div class="card-body text-center">
                            <h5 class="card-header header-bg-color mb-3">Number Of Tweets For Particualar Sentiment</h5>
                                <BarChart numberOfPositiveTweets={candidateSentimentDTO.amountOfPositveTweet} numberOfNegativeTweets={candidateSentimentDTO.amountOfNegativeTweet}
                                    numberOfNeutralTweets={candidateSentimentDTO.amountOfNeutralTweet} />
                            </div>
                        </div>
                    </div>
                </div>            
        </div>
    )
} 


//This is a functional component that display errors
const Error = ({ errorMessage }) => <div className="text-center title" role="alert">{errorMessage.response.data.error}</div>;

//This is an Higher order component
const withError = (Component) => ({ error, ...props }) =>
    error ? <Error errorMessage={error} /> : <Component {...props} />;

withError.propTypes = {
    Component: PropTypes.elementType.isRequired
}

/**
 * Render Component if there is no error else show error message
 */
const ErrorWithComponent = withError(RenderCandidateSentiment);

export default ErrorWithComponent;