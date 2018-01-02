/**
*
* UnassembledGroupCard
*
*/

import React from 'react';
import PropTypes from 'prop-types';
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
          title={this.props.name}
          hoverable
          className='group-card'
        >
          <Row type='flex' justify='space-between' style={{marginBottom: 8}}>
            <Col>Участников</Col>
            <Col>{this.props.members.count + '/' + this.props.size}</Col>
          </Row>
          <Row type='flex' justify='space-between' style={{marginBottom: 8}}>
            <Col>Взнос</Col>
            <Col>{this.props.totalValue} руб.</Col>
          </Row>
          <Row type='flex' justify='space-between' style={{marginBottom: 10}}>
            <Col>Тип</Col>
            <Col>{this.props.type}</Col>
          </Row>
          <Row gutter={6} type='flex' justify='start'>
            {this.props.tags.map(item =>
              <Link to="#">{item}</Link>
            )}
          </Row>
        </Card>
      </Col>
    );
  }
}

UnassembledGroupCard.defaultProps = {
  name: '',
  count: 0,
  size: 0,
  totalValue: 0,
  type: '',
  tags: []
}

UnassembledGroupCard.propTypes = {
  name: PropTypes.string,
  count: PropTypes.number,
  size: PropTypes.number,
  totalValue: PropTypes.number,
  type: PropTypes.string,
  tags: PropTypes.array,
};

export default UnassembledGroupCard;
