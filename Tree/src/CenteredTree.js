import React from "react";
import Tree from "react-d3-tree";
import NodeLabel from "./NodeLabel";

const containerStyles = {
  width: "100%",
  height: "100vh"
};


let myTreeData = [
  {
    name: "Rusta",
    attributes: {
      keyA: "val A",
      keyB: "val B",
      keyC: "val C",
    },
    children: [
      {
        name: "Germany",
        attributes: {
          code: "DE",
        },
        children: [
          {
            name: "Rusta Germany",
            attributes: {
              number: "01",
            },
            children: [
              {
                name: "10 Berlin",
                attributes: {
                  number: "10",
                },
                children: [
                  {
                    name: "POS 1",
                  },
                ],
              },
            ],
          },
        ],
      },
      {
        name: "Sweden",
        children: [
          {
            name: "Rusta Sweden",
            attributes: {
              number: "02",
            },
            children: [
              {
                name: "11 Stockholm",
                attributes: {
                  number: "11",
                },
              },
            ],
          },
        ],
      },
    ],
  },
];

export default class CenteredTree extends React.PureComponent {
  state = {
    treeData: []
   };

  componentDidMount() {
    fetch(`http://localhost:5198/Hierarchy`)
      .then((response) => response.json())
      .then((actualData) => {
        myTreeData = [actualData];

        this.setState({ treeData: [actualData] });
        console.log(this.state.treeData);
      })
      .finally(() => {
        const dimensions = this.treeContainer.getBoundingClientRect();
      this.setState({
        translate: {
          x: dimensions.width / 2,
          y: dimensions.height / 2,
        },
        nodeSize: {
          x: 400,
          y: 200,
        },
      });});

    this.setState({ treeData: [] });
    if (this.state.treeData.length > 0){
    const dimensions = this.treeContainer.getBoundingClientRect();
    this.setState({
      translate: {
        x: dimensions.width / 2,
        y: dimensions.height / 2,
      },
      nodeSize: {
        x: 400,
        y: 200,
      },
    });
  }

    console.log(this.state.treeData);
    
  }

  render() {
    if (this.state.treeData.length > 0){
      return (
        <div style={containerStyles} ref={(tc) => (this.treeContainer = tc)}>
          <Tree
            data={this.state.treeData}
            translate={this.state.translate}
            nodeSize={this.state.nodeSize}
            orientation={"horizontal"}
            initialDepth={0}
            allowForeignObjects
            nodeLabelComponent={{
              render: <NodeLabel className="myLabelComponentInSvg" />,
              foreignObjectWrapper: {
                y: -50,
                x: -125,
                width: 250,
                height: 150,
              },
            }}
          />
        </div>
      );
    }
    return (<div/>);
  }
}
