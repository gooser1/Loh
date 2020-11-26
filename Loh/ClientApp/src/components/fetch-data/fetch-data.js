import React from "react"
import mapCardToImgSrc from "../mappers/map-card-to-img-src"
import { ApiService } from "../services/api-service"
import "./fetch-data.css"

export default class FetchData extends React.Component {
  state = { cards: [], loading: true, sentCard: null, listView: true }

  apiService = new ApiService()

  componentDidMount() {
    this.apiService.getHand().then((data) => {
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

  changeCardView = () => {
    this.setState(({ listView }) => ({
      listView: !listView,
    }))
  }

  getImgItem = (card) => {
    const cardValue = mapCardToImgSrc(card)
    return (
      <div className="card-item" key={card.description}>
        <img
          onClick={() => this.sendCard(card)}
          src={cardValue}
          alt={card.description}
        ></img>
      </div>
    )
  }

  renderСardsField = (cards) => {
    return <div className="card-field">{cards.map(this.getImgItem)}</div>
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
    const { loading, cards, sentCard, listView } = this.state

    const contents = loading ? (
      <p>
        <em>Loading...</em>
      </p>
    ) : listView ? (
      this.renderСardsTable(cards)
    ) : (
      this.renderСardsField(cards)
    )

    const sentCardDescription = sentCard ? (
      <span> "{sentCard.description}" is sent</span>
    ) : null

    return (
      <div>
        <h1 id="tabelLabel">Fetch Data</h1>
        <button className="btn btn-secondary" onClick={this.changeCardView}>
          Change cards view
        </button>
        <span className="custom-warning">(click on card to post it)</span>
        {sentCardDescription}

        {contents}
      </div>
    )
  }
}
