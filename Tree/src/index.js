import React from "react";
import ReactDOM from "react-dom";
import Tree from "./CenteredTree";

import "./styles.css";

function App() {
  return (
    <div className="App">
      <h1>Organization Hierarchy</h1>
      <Tree />
    </div>
  );
}

const rootElement = document.getElementById("root");
ReactDOM.render(<App />, rootElement);
