import React, { Component } from "react";

export default class WebSocketExamplePage extends Component {
  static displayName = WebSocketExamplePage.name;
  socket;

  history = [];

  componentDidMount() {
    const scheme = document.location.protocol === "https:" ? "wss" : "ws";
    const port = document.location.port ? ":" + document.location.port : "";
    this.setState({
      connectionUrl: scheme + "://" + document.location.hostname + port + "/ws"
    });
  }

  componentWillUnmount() {
    if (this.socket?.readyState === WebSocket.CONNECTING) {
      this.socket.close();
    }
  }

  // state and state methods

  state = {
    messageControlsState: false,
    connectionControlsState: true,
    mainStateLabel: "Ready to connect...",
    connectionUrl: "",
    message: ""
  };

  setMessageControlsState = messageControlsState => {
    this.setState({
      messageControlsState
    });
  };

  setConnectionControlsState = connectionControlsState => {
    this.setState({
      connectionControlsState
    });
  };

  setStateLabel = mainStateLabel => {
    this.setState({
      mainStateLabel
    });
  };

  // socket methods

  connect = () => {
    this.setStateLabel("Connecting");
    this.socket = new WebSocket(this.state.connectionUrl);
    this.socket.onopen = event => {
      this.updateFormState();

      this.history.push(
        <tr key={this.history.length}>
          <td colSpan="3" className="commslog-data">
            Connection opened
          </td>
        </tr>
      );
      this.forceUpdate();
    };

    this.socket.onclose = event => {
      this.updateFormState();

      this.history.push(
        <tr key={this.history.length}>
          <td colSpan="3" className="commslog-data">
            Connection closed. Code: {event.code} . Reason:
            {event.reason}
          </td>
        </tr>
      );
      this.forceUpdate();
    };

    this.socket.onerror = event => {
      this.updateFormState();
    };

    this.socket.onmessage = event => {
      this.history.push(
        <tr key={this.history.length}>
          <td className="commslog-server">Server</td>
          <td className="commslog-client">Client</td>
          <td className="commslog-data">{event.data}</td>
        </tr>
      );
      this.forceUpdate();
    };
  };

  closeSocket = () => {
    if (!this.socket || this.socket.readyState !== WebSocket.OPEN) {
      alert("socket not connected");
    }
    this.socket.close(1000, "Closing from client");
  };

  sendMessage = () => {
    if (!this.socket || this.socket.readyState !== WebSocket.OPEN) {
      alert("socket not connected");
    }
    const data = this.state.message;
    this.socket.send(data);
    this.history.push(
      <tr key={this.history.length}>
        <td className="commslog-client">Client</td>
        <td className="commslog-server">Server</td>
        <td className="commslog-data">{data}</td>
      </tr>
    );
    this.forceUpdate();
  };

  updateFormState = () => {
    if (!this.socket) {
      this.setMessageControlsState(false);
    } else {
      switch (this.socket.readyState) {
        case WebSocket.CLOSED:
          this.setStateLabel("Closed");
          this.setMessageControlsState(false);
          this.setConnectionControlsState(true);
          break;
        case WebSocket.CLOSING:
          this.setStateLabel("Closing...");
          this.setMessageControlsState(false);
          this.setConnectionControlsState(false);
          break;
        case WebSocket.CONNECTING:
          this.setStateLabel("Connecting...");
          this.setMessageControlsState(false);
          this.setConnectionControlsState(false);
          break;
        case WebSocket.OPEN:
          this.setStateLabel("Open");
          this.setMessageControlsState(true);
          this.setConnectionControlsState(false);
          break;
        default:
          this.setStateLabel(
            `Unknown WebSocket State: ${this.socket.readyState}`
          );
          this.setMessageControlsState(false);
      }
    }
  };

  handleUrlChange = event => {
    this.setState({ connectionUrl: event.target.value });
  };

  handleMessageChange = event => {
    this.setState({ message: event.target.value });
  };

  htmlEscape(str) {
    return str
      .toString()
      .replace(/&/g, "&amp;")
      .replace(/"/g, "&quot;")
      .replace(/'/g, "&#39;")
      .replace(/</g, "&lt;")
      .replace(/>/g, "&gt;");
  }

  render() {
    const {
      messageControlsState,
      mainStateLabel,
      connectionControlsState,
      connectionUrl
    } = this.state;

    return (
      <div>
        <h1>WebSocket Sample Application</h1>
        <p id="stateLabel">{mainStateLabel}</p>
        <div>
          <label htmlFor="connectionUrl">WebSocket Server URL:</label>
          <input
            id="connectionUrl"
            disabled={!connectionControlsState}
            onChange={this.handleUrlChange}
            value={connectionUrl}
          />
          <button
            id="connectButton"
            type="submit"
            onClick={this.connect}
            disabled={!connectionControlsState}
          >
            Connect
          </button>
        </div>
        <p />
        <div>
          <label htmlFor="sendMessage">Message to send:</label>
          <input
            id="sendMessage"
            onChange={this.handleMessageChange}
            disabled={!messageControlsState}
          />
          <button
            id="sendButton"
            type="submit"
            disabled={!messageControlsState}
            onClick={this.sendMessage}
          >
            Send
          </button>
          <button
            id="closeButton"
            disabled={!messageControlsState}
            onClick={this.closeSocket}
          >
            Close Socket
          </button>
        </div>
        <h2>Communication Log</h2>
        <table style={{ width: 800 }}>
          <thead>
            <tr>
              <td style={{ width: 100 }}>From</td>
              <td style={{ width: 100 }}>To</td>
              <td>Data</td>
            </tr>
          </thead>
          <tbody id="commsLog">{this.history}</tbody>
        </table>
      </div>
    );
  }
}
