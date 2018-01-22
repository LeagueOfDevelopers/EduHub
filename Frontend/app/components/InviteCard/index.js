/**
*
* InviteCard
*
*/

import React from 'react';
// import styled from 'styled-components';
import {Card, Row, Col, Button} from 'antd';


class InviteCard extends React.PureComponent { // eslint-disable-line react/prefer-stateless-function
  constructor(props) {
    super(props)
  }

  render() {
    return (
      <Card
        hoverable
        className='notify-card'
        style={{width: '100%', cursor: 'default'}}
        bodyStyle={{padding: '14px 20px 0 20px'}}
      >
        {
          this.props.readed ? (<div className='readed-btn'/>) : (<div className='not-readed-btn'/>)
        }
        <Row style={{marginBottom: 12}}>
          <Col span={12}>
            <span style={{fontSize: 14, opacity: 0.9}}>{this.props.senderName}</span>
          </Col>
          <Col span={12} style={{textAlign: 'right'}}>
            <span style={{fontSize: 14, opacity: 0.7, marginRight: 12}}>
              {this.props.date}
            </span>
            <span style={{fontSize: 14, opacity: 0.7}}>
              {this.props.time}
            </span>
          </Col>
        </Row>
        <Row>
          <Col xs={{span: 24}} sm={{span: 12}} style={{marginBottom: 10}}>
            <span>
              {this.props.text}
            </span>
          </Col>
          <Col xs={{span: 24}} sm={{span: 12}} style={{textAlign: 'right'}}>
            <Button type='primary' style={{marginRight: 12, marginBottom: 14}}>Принять</Button>
            <Button>Отклонить</Button>
          </Col>
        </Row>
      </Card>
    );
  }
}

InviteCard.propTypes = {

};

export default InviteCard;
