/**
*
* UnassembledGroupCard
*
*/

import React from 'react';
import styled from 'styled-components';
import { Card, Col, Row } from 'antd';


class UnassembledGroupCard extends React.PureComponent { // eslint-disable-line react/prefer-stateless-function
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
            <Col>8/10</Col>
          </Row>
          <Row type='flex' justify='space-between' style={{marginBottom: 8}}>
            <Col>Взнос</Col>
            <Col>400 руб.</Col>
          </Row>
          <Row type='flex' justify='space-between' style={{marginBottom: 10}}>
            <Col>Тип</Col>
            <Col>Лекция</Col>
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

UnassembledGroupCard.propTypes = {

};

export default UnassembledGroupCard;
