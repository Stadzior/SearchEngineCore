import * as React from 'react';
import { RouteComponentProps } from 'react-router';

interface SearchPage {
    searchInput: string;
    searchResults: SearchResult[];
    loading: boolean;
}

interface SearchResult {
    url: string;
    pageRank: number;
    matchedWords: [string, number][];
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
        this.handleChange = this.handleChange.bind(this);
    }

    public render() {
        let contents = this.state.loading
            ? <p><em>Loading...</em></p>
            : this.renderSearchResults(this.state.searchResults);
        return (
            <div>
                <form onSubmit={this.runSearch}>
                    <input type="text" value={this.state.searchInput} onChange={this.handleChange}/>
                    <input type="submit" value="Search" />
                </form>
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
                </tr>
            </thead>
            <tbody>
                {searchResults.map(searchResult =>
                    <tr key={searchResult.url}>
                        <td>{searchResult.url}</td>
                        <td>
                            <ul>
                                {searchResult.matchedWords.map(matchedWord => <li>{matchedWord[0]}: {matchedWord[1]} times</li>)}
                            </ul>
                        </td>
                        <td>{searchResult.foundMatchInUrl}</td>
                        <td>{searchResult.pageRank}</td>
                    </tr>
                )}
            </tbody>
        </table>;
    }

    handleChange(event: { target: { value: string; }; }) {
        this.setState({ searchInput: event.target.value });
    }

    runSearch() {
        this.setState({
            loading: true
        });
        fetch('api/Search/GetResults')
            .then(response => response.json() as Promise<SearchResult[]>)
            .then(data => {
                this.setState({ searchResults: data, loading: false });
            });
    }
}
