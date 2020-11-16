import React from "react"
import { ApiService } from "../api-service"
import "./fetch-data.css"

export default class FetchData extends React.Component {
  state = { cards: [], loading: true, sentCard: null, postAnswer: null }

  apiService = new ApiService()

  componentDidMount() {
    this.apiService.getAllCards().then((data) => {
      this.setState({ cards: data, loading: false })
    })
  }

  sendCard = (card) => {
    this.apiService.sendCard(card).then(
      (data) => console.log("data: ", data),
      (err) => console.log("err: ", err)
    )
    this.setState({ sentCard: card })
    const interval = setInterval(() => {
      this.setState({ sentCard: null })
      clearInterval(interval)
    }, 5000)
  }

  getTableItem = (card) => {
    return (
      <tr key={card.description}>
        <td onClick={() => this.sendCard(card)} className="card-item">
          {card.description}
        </td>
      </tr>
    )
  }

  renderСardsTable = (cards) => {
    return (
      <table className="table table-striped" aria-labelledby="tabelLabel">
        <thead>
          <tr>
            <th>Description</th>
          </tr>
        </thead>
        <tbody>{cards.map(this.getTableItem)}</tbody>
      </table>
    )
  }

  render() {
    const { loading, cards, sentCard } = this.state

    const contents = loading ? (
      <p>
        <em>Loading...</em>
      </p>
    ) : (
      this.renderСardsTable(cards)
    )

    const sentCardDescription = sentCard ? (
      <span> "{sentCard.description}" is sent</span>
    ) : null

    return (
      <div>
        <h1 id="tabelLabel">Fetch Data</h1>
        <span className="custom-warning">(click on card to post it)</span>
        {sentCardDescription}
        {contents}
      </div>
    )
  }
}
