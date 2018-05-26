/**
*
* AssembledGroupCard
*
*/

import React from 'react';
import PropTypes from 'prop-types';
import {getGroupType} from '../../globalJS';
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
        >
          <Row type='flex' justify='space-between' style={{marginBottom: 8}}>
            <Col>Участников</Col>
            <Col>{this.props.groupInfo.memberAmount + '/' + this.props.groupInfo.size}</Col>
          </Row>
          <Row type='flex' justify='space-between' style={{marginBottom: 8, flexWrap: 'nowrap'}}>
            <Col style={{marginRight: 20}}>Оплата</Col>
            <Col style={{textOverflow: 'ellipsis', whiteSpace: 'nowrap', overflow: 'hidden'}}>{this.props.groupInfo.cost} руб.</Col>
          </Row>
          <Row type='flex' justify='space-between' style={{marginBottom: 10}}>
            <Col>Тип</Col>
            <Col>{getGroupType(this.props.groupInfo.groupType)}</Col>
          </Row>
          <Row gutter={6} className='group-tags'>
            {this.props.groupInfo.tags.map(item =>
              <Link to={`/groups?tags=${item.replace('#', '*')}`} key={item}>{item}</Link>
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
    memberAmount: 0,
    size: 0,
    cost: 0,
    groupType: '',
    tags: []
  }
};

AssembledGroupCard.propTypes = {
  groupInfo: PropTypes.object,
  title: PropTypes.string,
  size: PropTypes.number,
  cost: PropTypes.number,
  groupType: PropTypes.string,
  tags: PropTypes.array,
  memberAmount: PropTypes.number
};

export default AssembledGroupCard;
