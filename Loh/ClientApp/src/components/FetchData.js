import React, { Component } from 'react';

export class FetchData extends Component {
  static displayName = FetchData.name;

  constructor(props) {
    super(props);
    this.state = { cards: [], loading: true };
  }

  componentDidMount() {
      this.PopulateDeck();
  }

  static rendercardsTable(cards) {
    return (
      <table className='table table-striped' aria-labelledby="tabelLabel">
        <thead>
          <tr>
                    <th>Description</th>
          </tr>
        </thead>
        <tbody>
          {cards.map(card =>
              <tr key={card.description}>
                  <td>{card.description}</td>
            </tr>
          )}
        </tbody>
      </table>
    );
  }

  render() {
    let contents = this.state.loading
      ? <p><em>Loading...</em></p>
      : FetchData.rendercardsTable(this.state.cards);

    return (
      <div>
        <h1 id="tabelLabel" >Новая колода</h1>
        {contents}
      </div>
    );
  }

  async PopulateDeck() {
    const response = await fetch('Cards');
    const data = await response.json();
    this.setState({ cards: data, loading: false });
  }
}
