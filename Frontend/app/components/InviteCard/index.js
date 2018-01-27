/**
*
* InviteCard
*
*/

import React from 'react';
import { connect } from 'react-redux';
import { createStructuredSelector } from 'reselect';
import { compose } from 'redux';

import injectSaga from 'utils/injectSaga';
import injectReducer from 'utils/injectReducer';
import reducer from '../../containers/NotificationPage/reducer';
import saga from '../../containers/NotificationPage/saga';
import { changeInvitationStatus } from "../../containers/NotificationPage/actions";
// import styled from 'styled-components';
import {Card, Row, Col, Button, message} from 'antd';


class InviteCard extends React.PureComponent { // eslint-disable-line react/prefer-stateless-function
  constructor(props) {
    super(props)
  }

  tryAccept() {
    (localStorage.getItem('without_server') === 'true') ?
      message.success('Приглашение принято')
      :
      this.props.acceptInvitation(this.props.groupId, this.props.id, 'Accepted')
  }

  tryDecline() {
    (localStorage.getItem('without_server') === 'true') ?
      message.success('Приглашение отклонено')
      :
      this.props.declineInvitation(this.props.groupId, this.props.id, 'Declined')
  }

  render() {
    return (
      <Card
        hoverable
        className='notify-card'
        style={{width: '100%', cursor: 'default'}}
        bodyStyle={{padding: '14px 20px 0 20px'}}
      >
        {/*{*/}
          {/*this.props.readed ? (<div className='readed-btn'/>) : (<div className='not-readed-btn'/>)*/}
        {/*}*/}
        <Row style={{marginBottom: 12}}>
          <Col span={12}>
            <span style={{fontSize: 14, opacity: 0.9}}>{this.props.fromUser}</span>
          </Col>
          {/*<Col span={12} style={{textAlign: 'right'}}>*/}
            {/*<span style={{fontSize: 14, opacity: 0.7}}>*/}
              {/*{this.props.date}*/}
            {/*</span>*/}
          {/*</Col>*/}
        </Row>
        <Row>
          <Col xs={{span: 24}} sm={{span: 12}} style={{marginBottom: 10}}>
            <span>
              Вас пригласили в группу {this.props.groupId} на роль "{this.props.suggestedRole}"
            </span>
          </Col>
          <Col xs={{span: 24}} sm={{span: 12}} style={{textAlign: 'right'}}>
            <Button
              type='primary'
              style={{marginRight: 12, marginBottom: 14}}
              onClick={this.tryAccept}
            >
              Принять</Button>
            <Button onClick={this.tryDecline}>Отклонить</Button>
          </Col>
        </Row>
      </Card>
    );
  }
}

InviteCard.propTypes = {

};

function mapDispatchToProps(dispatch) {
  return {
    acceptInvitation: (groupId, invitationId, status) => dispatch(changeInvitationStatus(groupId, invitationId, status)),
    declineInvitation: (groupId, invitationId, status) => dispatch(changeInvitationStatus(groupId, invitationId, status))
  };
}

const mapStateToProps = createStructuredSelector({
});

const withConnect = connect(mapStateToProps, mapDispatchToProps);

const withReducer = injectReducer({ key: 'notificationPage', reducer });
const withSaga = injectSaga({ key: 'notificationPage', saga });

export default compose(
  withReducer,
  withSaga,
  withConnect,
)(InviteCard);
