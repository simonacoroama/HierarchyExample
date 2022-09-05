import React from 'react';
import Tree from 'react-d3-tree';

import { useState, useEffect } from "react";

export default function OrgChartTree() {
    const [products, setProducts] = useState([]);

    useEffect(() => {
        fetch(`http://localhost:5198/Hierarchy`)
        .then((response) => response.json())
        .then((actualData) => {
            setProducts(actualData)
    })
       }, []);

    return (
      // `<Tree />` will fill width/height of its container; in this case `#treeWrapper`.
      <div id="treeWrapper" style={{ width: '500em', height: '200em' }}>
        <Tree data={products} orientation="vertical" initialDepth={1}/>
      </div>
    );
  }