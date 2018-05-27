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
    matchedWords: number;
    summary: string;
}

export class Home extends React.Component<RouteComponentProps<{}>, SearchPage> {
    constructor() {
        super();
        this.state = { searchInput: "", searchResults: [], loading = true };

        fetch('api/Search/GetResults')
            .then(response => response.json() as Promise<SearchResult[]>)
            .then(data => {
                this.setState({ searchResults: data, loading: false });
            });
    }

    public render() {
        let contents = this.state.loading
            ? <p><em>Loading...</em></p>
            : this.renderSearchResults(this.state.searchResults);
        return (
            <div>
                <h1>Search results:</h1>
                {contents}
            </div>);
            <form onSubmit={this.handleSubmit}>
                <input type="text" value={this.state.searchInput} onChange={this.handleChange} />
                <input type="submit" value="Submit" />
            </form>
        );
    }

    renderSearchResults(results: SearchResult[]) {
        return <table className='table'>
            <thead>
                <tr>
                    <th>Date</th>
                    <th>Temp. (C)</th>
                    <th>Temp. (F)</th>
                    <th>Summary</th>
                </tr>
            </thead>
            <tbody>
                {forecasts.map(forecast =>
                    <tr key={forecast.dateFormatted}>
                        <td>{forecast.dateFormatted}</td>
                        <td>{forecast.temperatureC}</td>
                        <td>{forecast.temperatureF}</td>
                        <td>{forecast.summary}</td>
                    </tr>
                )}
            </tbody>
        </table>;
    }

    runSearch() {
        this.setState({
            searchInput: this.state.searchInput
        });
    }
}
