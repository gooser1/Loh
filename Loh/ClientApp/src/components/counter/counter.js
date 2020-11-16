import React, { Component } from "react"

export default class Counter extends Component {
  static displayName = Counter.name

  constructor(props) {
    super(props)
    this.state = { currentCount: 0 }
    this.incrementCounter = this.incrementCounter.bind(this)
  }

  incrementCounter() {
    this.setState({
      currentCount: this.state.currentCount + 1,
    })
  }

  render() {
    return (
      <div>
        <h1>Counter</h1>

        <p aria-live="polite">
          Current counter value: <strong>{this.state.currentCount}</strong>
        </p>

        <button className="btn btn-primary" onClick={this.incrementCounter}>
          {" "}
          +{" "}
        </button>
      </div>
    )
  }
}
