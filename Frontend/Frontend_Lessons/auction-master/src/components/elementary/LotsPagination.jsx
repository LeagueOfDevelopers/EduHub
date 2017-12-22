import React, { Component } from 'react';
import { Pagination } from 'antd';

class LotsPagination extends React.Component {
  state = {
    current: 1,
  }
  onChange = (page) => {
    console.log(page);
    this.setState({
      current: page,
    });
  }
  render() {
    return (
        <Pagination current={this.state.current} onChange={this.onChange} total={50} />
    );
  }
}

export default LotsPagination;