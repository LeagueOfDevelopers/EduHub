/**
*
* AssembledGroupCard
*
*/

import React from 'react';
import styled from 'styled-components';
import { Card, Col, Row } from 'antd';


class AssembledGroupCard extends React.PureComponent { // eslint-disable-line react/prefer-stateless-function
  constructor(props) {
    super(props)
  }

  render() {
    return (
      <Col>
        <Card
          title={this.props.title}
          hoverable
          className='group-card'
        >
          <Row type='flex' justify='space-between' style={{marginBottom: 8}}>
            <Col>Участников</Col>
            <Col>10/10</Col>
          </Row>
          <Row type='flex' justify='space-between' style={{marginBottom: 8}}>
            <Col>Оплата</Col>
            <Col>4200 руб.</Col>
          </Row>
          <Row type='flex' justify='space-between' style={{marginBottom: 10}}>
            <Col>Тип</Col>
            <Col>{this.props.type}</Col>
          </Row>
          <Row gutter={6} type='flex' justify='start'>
            <a>#тэг1</a>
            <a>#второйтэг</a>
            <a>#третийтэг</a>
            <a>#четвертыйтэг</a>
          </Row>
        </Card>
      </Col>
    );
  }
}

AssembledGroupCard.defaultProps = {
  type: 'Лекция'
}

AssembledGroupCard.propTypes = {

};

export default AssembledGroupCard;
