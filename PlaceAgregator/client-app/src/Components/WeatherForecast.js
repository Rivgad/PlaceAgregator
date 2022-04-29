import React, { Component } from 'react';
import Button from '@mui/material/Button';

export default class WeatherForecastComponent extends Component {
    static displayName = WeatherForecastComponent.name;

    constructor(props) {
        super(props);
        this.state = { loading: true };
    }

    render() {
        let contents = this.state.loading
            ? <p><em>Загрузка...</em></p>
            : <p>Загрузилось!</p>;

        return (
            <div>
                <h1 id="tabelLabel" >Weather forecast</h1>
                <p>This component demonstrates fetching data from the server.</p>
                {contents}
                <Button variant="contained" onClick={()=>this.populateWeatherData()}>Обновить данные</Button>
            </div>
        );
    }

    async populateWeatherData() {
        this.setState({ loading: false });
        setTimeout(() => { this.setState({ loading: true }) }, 1000);
    }
}
