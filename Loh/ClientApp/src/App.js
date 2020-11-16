import React, { Component } from "react"
import { Route } from "react-router"
import Counter from "./components/counter/counter"
import FetchData from "./components/fetch-data/fetch-data"
import Home from "./components/home/home"
import { Layout } from "./components/layout"

import "./custom.css"

export default class App extends Component {
  static displayName = App.name

  render() {
    return (
      <Layout>
        <Route exact path="/" component={Home} />
        <Route path="/counter" component={Counter} />
        <Route path="/fetch-data" component={FetchData} />
      </Layout>
    )
  }
}
