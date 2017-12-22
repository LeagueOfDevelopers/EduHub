import React, { Component } from 'react';
import { Link } from 'react-router-dom';
import PropTypes from 'prop-types';
import { Table, Icon } from 'antd';

const columns = [{
  title: 'Название лота',
  dataIndex: 'lotId',
  key: 'lotId',
  render: text => <Link to={`lots/${this.props.lotId}`}>{text}</Link>,
}, {
  title: 'Статус лота',
  dataIndex: 'status',
  key: '#',
}, {
  title: 'Результат торга',
  dataIndex: 'result',
  key: '#',
}, {
  title: 'Ваша ставка',
  dataIndex: 'bet',
  key: 'betId'
}];
{/*
const data = [{
  key: '1',
  name: 'John Brown',
  age: 32,
  address: 'New York No. 1 Lake Park',
}, {
  key: '2',
  name: 'Jim Green',
  age: 42,
  address: 'London No. 1 Lake Park',
}, {
  key: '3',
  name: 'Joe Black',
  age: 32,
  address: 'Sidney No. 1 Lake Park',
}];
*/}
class MadeBetsTable extends React.Component {
  constructor(props) {
    super(props);
  }
  static defaultProps = {
    data: []
  }
  static PropTypes = {
      data: PropTypes.array
      /*lotId: PropTypes.number,
      title: PropTypes.string,
      timer: PropTypes.number,
      bet: PropTypes.number*/
  }
  render() {
    return (
      <Table columns={columns} dataSource={this.props.data} />
    )
  }
}

export default MadeBetsTable;