import * as React from 'react';
import { RouteComponentProps } from 'react-router';

interface SearchPage {
    searchInput: string;
    searchResults: SearchResult[];
    loading: boolean;
}

interface SearchResult {
    url: string;
    pageRank: string;
    rating: string;
    matchedWords: string[];
    foundMatchInUrl: boolean;
}

export class Home extends React.Component<RouteComponentProps<{}>, SearchPage> {
    constructor() {
        super();
        this.state = {
            searchInput: "",
            searchResults: [],
            loading: false
        };
    }


    public render() {
        let contents = this.state.loading
            ? <p><em>Loading...</em></p>
            : this.renderSearchResults(this.state.searchResults);
        return (
            <div>
                <input type="text" value={this.state.searchInput} onChange={this.handleChange}/>
                <button onClick={() => { this.runSearch() }}>Search</button>
                <h1>Search results:</h1>
                {contents}
            </div>
        );
    }

    renderSearchResults(searchResults: SearchResult[]) {
        return <table className='table'>
            <thead>
                <tr>
                    <th>Url</th>
                    <th>Matched words</th>
                    <th>Found match in title</th>
                    <th>Page rank</th>
                    <th>Rating</th>
                </tr>
            </thead>
            <tbody>
                {searchResults.map(searchResult =>
                    <tr key={searchResult.url}>
                        <td><a href={searchResult.url}>{searchResult.url}</a></td>
                        <td>
                            <ul>
                                {searchResult.matchedWords.map(matchedWord => <li>{String(matchedWord)}</li>)}
                            </ul>
                        </td>
                        <td>{String(searchResult.foundMatchInUrl)}</td>
                        <td>{searchResult.pageRank}</td>
                        <td>{searchResult.rating}</td>
                    </tr>
                )}
            </tbody>
        </table>;
    }

    handleChange = (event: { target: { value: string; }; }) => {
        this.setState({ searchInput: event.target.value });
    }

    runSearch = () => {
        this.setState({
            loading: true
        });
        let searchInput = this.state.searchInput.replace(' ', '+');   
        console.log(searchInput)
        fetch('api/Search/GetResults?searchInput=' + searchInput)
            .then(response =>
                response.json() as Promise<SearchResult[]>)
            .then(data => {
                console.log(data)
                this.setState({ searchResults: data, loading: false });
            });
    }
}
