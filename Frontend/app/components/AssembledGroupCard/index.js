/**
*
* AssembledGroupCard
*
*/

import React from 'react';
import PropTypes from 'prop-types';
// import styled from 'styled-components';
import { Card, Col, Row } from 'antd';
import {Link} from "react-router-dom";


class AssembledGroupCard extends React.PureComponent { // eslint-disable-line react/prefer-stateless-function
  constructor(props) {
    super(props);
  }

  render() {
    return (
      <Col>
        <Card
          title={this.props.groupInfo.title}
          hoverable
          className='group-card'
        >
          <Row type='flex' justify='space-between' style={{marginBottom: 8}}>
            <Col>Участников</Col>
            <Col>{this.props.numberOfMembers + '/' + this.props.groupInfo.size}</Col>
          </Row>
          <Row type='flex' justify='space-between' style={{marginBottom: 8}}>
            <Col>Оплата</Col>
            <Col>{this.props.groupInfo.moneyPerUser} руб.</Col>
          </Row>
          <Row type='flex' justify='space-between' style={{marginBottom: 10}}>
            <Col>Тип</Col>
            <Col>{this.props.groupInfo.groupType}</Col>
          </Row>
          <Row gutter={6} type='flex' justify='start'>
            {this.props.groupInfo.tags.map(item =>
              <Link to="#">{item}</Link>
            )}
          </Row>
        </Card>
      </Col>
    );
  }
}

AssembledGroupCard.defaultProps = {
  groupInfo: {
    title: '',
    numberOfMembers: 0,
    size: 0,
    moneyPerUser: 0,
    groupType: '',
    tags: []
  }
};

AssembledGroupCard.propTypes = {
  groupInfo: PropTypes.object,
  title: PropTypes.string,
  size: PropTypes.number,
  moneyPerUser: PropTypes.number,
  groupType: PropTypes.string,
  tags: PropTypes.array,
  numberOfMembers: PropTypes.number
};

export default AssembledGroupCard;
