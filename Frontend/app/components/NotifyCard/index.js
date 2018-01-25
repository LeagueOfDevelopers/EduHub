/**
*
* NotifyCard
*
*/

import React from 'react';
// import styled from 'styled-components';
import {Card, Row, Col} from 'antd';

class NotifyCard extends React.PureComponent { // eslint-disable-line react/prefer-stateless-function
  constructor(props) {
    super(props)
  }

  render() {
    return (
      <Card
        hoverable
        className='notify-card'
        style={{width: '100%', cursor: 'default', position: 'relative', overflow: 'visible'}}
        bodyStyle={{padding: '14px 20px'}}
      >
        {
          this.props.readed ? (<div className='readed-btn'/>) : (<div className='not-readed-btn'/>)
        }
        <Row style={{marginBottom: 12}}>
          <Col span={12}>
            <span style={{fontSize: 14, opacity: 0.9}}>{this.props.fromUser}</span>
          </Col>
          <Col span={12} style={{textAlign: 'right'}}>
            <span style={{fontSize: 14, opacity: 0.7}}>
              {this.props.date}
            </span>
          </Col>
        </Row>
        <Row>
          <span>
            {this.props.text}
          </span>
        </Row>
      </Card>
    );
  }
}

NotifyCard.propTypes = {

};

export default NotifyCard;
