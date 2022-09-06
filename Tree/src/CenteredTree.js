import React from "react";
import Tree from "react-d3-tree";
import NodeLabel from "./NodeLabel";

const containerStyles = {
  width: "100%",
  height: "100vh"
};

const myTreeData = [
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
  state = {};

  componentDidMount() {
    const dimensions = this.treeContainer.getBoundingClientRect();
    this.setState({
      translate: {
        x: dimensions.width / 2,
        y: dimensions.height / 2
      },
      nodeSize: {
        x: 400,
        y: 200
      }
    });
  }

  render() {
    return (
      <div style={containerStyles} ref={tc => (this.treeContainer = tc)}>
        <Tree
          data={myTreeData}
          translate={this.state.translate}
          nodeSize={this.state.nodeSize}
          orientation={"horizontal"}
          allowForeignObjects
          nodeLabelComponent={{
            render: <NodeLabel className="myLabelComponentInSvg" />,
            foreignObjectWrapper: {
              y: -50,
              x: -125,
              width: 250,
              height: 150
            }
          }}
        />
      </div>
    );
  }
}
