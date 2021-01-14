import React, { Component } from "react";
import { Route } from "react-router";
import Counter from "./components/counter/counter";
import FetchData from "./components/fetch-data/fetch-data";
import Home from "./components/home/home";
import { Layout } from "./components/Layout";
import WebSocketExamplePage from "./components/web-socket-example/web-socket-example";

import "./custom.css";

export default class App extends Component {
  static displayName = App.name;

  render() {
    return (
      <Layout>
        <Route exact path="/" component={Home} />
        <Route path="/counter" component={Counter} />
        <Route path="/fetch-data" component={FetchData} />
        <Route path="/web-socket-sample" component={WebSocketExamplePage} />
      </Layout>
    );
  }
}
