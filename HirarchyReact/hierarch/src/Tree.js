import React from 'react';
import Tree from 'react-d3-tree';

import { useState, useEffect } from "react";

export default function OrgChartTree() {
    const [products, setProducts] = useState();
    const [loading, setLoading] = useState(true);

    useEffect(() => {
        fetch(`http://localhost:5198/Hierarchy`)
        .then((response) => {
          return response.json();
        })
        .then((actualData) => {
             return setProducts(actualData);
    })
    .finally(() => {
      return setLoading(false);
  });
       }, []);

  if(loading){
    return (<div>loading...</div>);
  }

    return (
      // `<Tree />` will fill width/height of its container; in this case `#treeWrapper`.
      <div style={{margin:'1em'}}>
      <div id="treeWrapper" style={{ width: '500em', height: '200em' }}>
        <Tree data={products} orientation="vertical" initialDepth={0} style={{margin:'1em'}}/>
      </div>
      </div>
    );
  }