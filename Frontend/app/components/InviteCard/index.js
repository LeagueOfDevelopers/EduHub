/**
*
* InviteCard
*
*/

import React from 'react';
import PropTypes from 'prop-types';
import { connect } from 'react-redux';
import { createStructuredSelector } from 'reselect';
import { compose } from 'redux';
import injectSaga from 'utils/injectSaga';
import injectReducer from 'utils/injectReducer';
import reducer from '../../containers/NotificationPage/reducer';
import saga from '../../containers/NotificationPage/saga';
import { changeInvitationStatus } from "../../containers/NotificationPage/actions";
import { getMemberRole } from "../../globalJS";
import {Card, Row, Col, Button, message} from 'antd';


class InviteCard extends React.PureComponent { // eslint-disable-line react/prefer-stateless-function
  constructor(props) {
    super(props);

    this.tryAccept = this.tryAccept.bind(this);
    this.tryDecline = this.tryDecline.bind(this);
  }

  tryAccept() {
    (localStorage.getItem('without_server') !== 'true') ?
      this.props.acceptInvitation(this.props.id, 'Accepted') : null;
  }

  tryDecline() {
    (localStorage.getItem('without_server') !== 'true') ?
      this.props.declineInvitation(this.props.id, 'Declined') : null;
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
            <span style={{fontSize: 14, opacity: 0.9}}>{this.props.fromUser}</span>
          </Col>
          <Col span={12} style={{textAlign: 'right'}}>
            <span style={{fontSize: 14, opacity: 0.7}}>
              {this.props.date}
            </span>
          </Col>
        </Row>
        <Row>
          <Col xs={{span: 24}} sm={{span: 12}} style={{marginBottom: 10}}>
            <span style={{wordWrap: 'break-word'}}>
              Вас пригласили в группу {this.props.groupId} на роль "{getMemberRole(this.props.suggestedRole)}"
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
  readed: PropTypes.bool,
  groupId: PropTypes.string,
  id: PropTypes.string,
  fromUser: PropTypes.string,
  date: PropTypes.string,
  suggestedRole: PropTypes.oneOfType([
    PropTypes.number,
    PropTypes.string
  ])

};

function mapDispatchToProps(dispatch) {
  return {
    acceptInvitation: (invitationId, status) => dispatch(changeInvitationStatus(invitationId, status)),
    declineInvitation: (invitationId, status) => dispatch(changeInvitationStatus(invitationId, status))
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
