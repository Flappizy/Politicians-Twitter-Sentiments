import React, { Component } from 'react';
import axios from 'axios';
import ErrorWithComponent from './Error';
import Loading from './Loading';
import './Home.css';

const PATH = 'api/home/getsentiment';
const PARAM_QUERY = 'candidate=';

//This is the Search Component
const Search = ({ searchTerm, onChange, children, onSubmit }) =>
    <div className="over mb-3">
        <div className="row">
            <form className="title over input-group" onSubmit={onSubmit}>
                <label>
                    <input className="form-control" type="text" value={searchTerm} onChange={onChange} placeholder="Search" />
                </label>
                <button className="btn header-bg-color" type="submit">{children}</button>
            </form>
        </div>
    </div>
    

export class Home extends Component {
    //This property is used to keep track of if the component is mounted or not inorder to prevent an
    //error that happens in the process of when a component unmounts then it still goes and do a background job
    //which is not appropriate
    _isMounted = false;

    constructor(props) {
        super(props);
        this.state = {
            candidateSentimentDTO: {},
            isLoading: false,
            searchTerm: null,
            error: null
        };
        this.searchTermChanged = this.onSearchTermChanged.bind(this);
        this.onSearchSubmitted = this.onSearchSubmit.bind(this);
    }

    componentDidMount() {
        this._isMounted = true;
    }

    componentWillUnmount() {
        this._isMounted = false;
    }

    //This is the event handler when the user clicks submit
    onSearchSubmit(event) {
        const { searchTerm } = this.state;

        this.getCandidateSentimentData();
        event.preventDefault();
    }

    //This method is used to bind the value input by the user to the searchTerm property
    onSearchTermChanged(event) {
        this.setState({ searchTerm: event.target.value });
    }

    setSentimentResult(result) {
        this.setState({
            candidateSentimentDTO: result,
            isLoading: false,
        });
        console.log(result);
    }

    async getCandidateSentimentData() {

        const { searchTerm } = this.state;
        //Sets Loading property to true
        this.setState({ isLoading: true, error: null, candidateSentimentDTO: {} });

        //The api call happens here
        axios.get(`${PATH}?${PARAM_QUERY}${searchTerm}`)
            .then(result => this._isMounted && this.setSentimentResult(result.data))
            .catch(error => this._isMounted && this.setState({ error }));
    }

    render() {
        const {
            candidateSentimentDTO,
            searchTerm,
            error,
            isLoading,
        } = this.state;

        let content = Object.keys(candidateSentimentDTO).length === 0 && !isLoading
            ?
            <div className="mt-5 title">
                <em>This is a simple tool that gives you an approximate twitter opinion of the below politicians names.
                    The auhor of this tool, in his opinion, think one of the listed politicians will be the next Nigerian president
                    <div>
                        <ul>
                            <li>Bola Ahmed Tinubu</li>
                            <li>Abubakar Atiku</li>
                            <li>Yemi Osinbajo</li>
                            <li>Peter Obi</li>
                            <li>Bukola Saraki</li>
                        </ul>
                    </div>
                    So  all you have to do is search one of the above names and you get the twitter sentiment of that candidate
                </em>
            </div>
            :
            isLoading && Object.keys(candidateSentimentDTO).length === 0 && !error
                ?
                <Loading />
                :
                <ErrorWithComponent
                    error={error}
                    candidateSentimentDTO={candidateSentimentDTO}
                />;

        return (
            <div>
                <div className="interactions">
                    <Search searchTerm={searchTerm} onSubmit={this.onSearchSubmitted} onChange={this.searchTermChanged}>
                        Search</Search>
                </div>

                <div>
                    {content}
                </div>
            </div>
        );
    }
}
