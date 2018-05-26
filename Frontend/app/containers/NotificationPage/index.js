/**
 *
 * NotificationPage
 *
 */

import React from 'react';
import PropTypes from 'prop-types';
import { connect } from 'react-redux';
import { createStructuredSelector } from 'reselect';
import { compose } from 'redux';
import injectSaga from 'utils/injectSaga';
import injectReducer from 'utils/injectReducer';
import {makeSelectNotifies, makeSelectInvites, makeSelectNeedUpdate} from './selectors';
import reducer from './reducer';
import saga from './saga';
import {getInvites, getNotifies, setNotifySetting} from "./actions";
import NotifyCard from '../../components/NotifyCard';
import InviteCard from '../../components/InviteCard';
import {Row, Col, Tabs, Button, Icon, Select, Divider, Form} from 'antd';
import {getNotifySettingType, getNotifySettingTypeRevert} from "../../globalJS";
import config from "../../config";
const TabPane = Tabs.TabPane;
const FormItem = Form.Item;
const Option = Select.Option;

const notifies = [
  {
    id: '1233123124',
    fromUser: 'Имя отправителя',
    date: new Date().toDateString(),
    text: 'Текст уведомления',
    readed: false
  },
  {
    id: 'dasqdsad',
    fromUser: 'Имя отправителя',
    date: new Date().toDateString(),
    text: 'Текст уведомления',
    readed: true
  },
  {
    id: 'qwersifhxjk',
    fromUser: 'Имя отправителя',
    date: new Date().toDateString(),
    text: 'Текст уведомления',
    readed: true
  }
];

const invites = [
  {
    id: 'dasqdsadsdfsdf',
    fromUser: 'fdspijfjlkf',
    fromUserName: 'Имя отправителя',
    date: new Date().toDateString(),
    toGroup: '32478643981654',
    toGroupTitle: 'JS Juniors',
    suggestedRole: 'Участник',
    readed: false
  },
  {
    id: 'dasqdsddsfwee',
    fromUser: '324gj3h4j1',
    fromUserName: 'Имя отправителя',
    date: new Date().toDateString(),
    toGroup: 'dr32847363274',
    toGroupTitle: 'C# Juniors',
    suggestedRole: 'Участник',
    readed: true
  }
];

export class NotificationPage extends React.Component { // eslint-disable-line react/prefer-stateless-function
  constructor(props) {
    super(props);

    this.state = {
      needUpdate: this.props.needUpdate,
      isEditing: false,
      CourseFinished: 4,
      CurriculumAccepted: 4,
      CurriculumDeclined: 4,
      CurriculumSuggested: 4,
      GroupIsFormed: 4,
      InvitationAccepted: 4,
      InvitationDeclined: 4,
      InvitationReceived: 4,
      MemberLeft: 4,
      NewCreator: 4,
      NewMember: 4,
      ReviewReceived: 4,
      SanctionsAppliedToUser: 4,
      SanctionsCancelledToUser: 4,
      TeacherFound: 4
    };

    this.showSettingBtn = this.showSettingBtn.bind(this);
    this.hideSettingBtn = this.hideSettingBtn.bind(this);
    this.onHandleSettingChange = this.onHandleSettingChange.bind(this);
    this.getNotifiesSettings = this.getNotifiesSettings.bind(this);
    this.onSetResult = this.onSetResult.bind(this);
  }

  getNotifiesSettings = () => {
    return fetch(`${config.API_BASE_URL}/user/profile/notifications/settings`, {
      method: 'GET',
      headers: {
        'Content-Type': 'application/json-patch+json',
        'Authorization': `Bearer ${localStorage.getItem('token')}`
      }
    })
      .then(res => res.json())
      .then(res => this.onSetResult(res))
      .catch(error => error)
  };

  onSetResult = (result) => {
    this.setState({
      CourseFinished: result.CourseFinished,
      CurriculumAccepted: result.CurriculumAccepted,
      CurriculumDeclined: result.CurriculumDeclined,
      CurriculumSuggested: result.CurriculumSuggested,
      GroupIsFormed: result.GroupIsFormed,
      InvitationAccepted: result.InvitationAccepted,
      InvitationDeclined: result.InvitationDeclined,
      InvitationReceived: result.InvitationReceived,
      MemberLeft: result.MemberLeft,
      NewCreator: result.NewCreator,
      NewMember: result.NewMember,
      ReviewReceived: result.ReviewReceived,
      SanctionsAppliedToUser: result.SanctionsAppliedToUser,
      SanctionsCancelledToUser: result.SanctionsCancelledToUser,
      TeacherFound: result.TeacherFound
    })
  };

  componentDidMount() {
    if(localStorage.getItem('without_server') === 'true') {
      this.setState({
        notifies: this.notifies,
        invites: this.invites
      })
    }
    else {
      this.props.getNotifies();
      this.props.getInvites();
      this.getNotifiesSettings();
    }
  }

  componentDidUpdate(prevProps, prevState) {
    if(prevProps.needUpdate !== this.props.needUpdate) {
      this.componentDidMount()
    }
  }

  showSettingBtn = () => {
    document.getElementById('setting-btn').classList.remove('notify-setting-btn-hidden');
    document.getElementById('setting-btn').classList.add('notify-setting-btn');
  };

  hideSettingBtn = () => {
    document.getElementById('setting-btn').classList.remove('notify-setting-btn');
    document.getElementById('setting-btn').classList.add('notify-setting-btn-hidden');
  };

  onHandleSettingChange = (notifyType, settingType) => {
    this.setState({[notifyType]: getNotifySettingTypeRevert(settingType)});
    setTimeout(() => this.props.setNotifySetting(notifyType, settingType), 0);
  };

  render() {
    return (
      <Row className='notify-tabs'>
        {
          !this.state.isEditing ?
            <Col className='notify-area' onMouseEnter={this.showSettingBtn} onMouseLeave={this.hideSettingBtn} xs={{span: 22, offset: 1}} sm={{span: 20, offset: 2}} lg={{span: 12, offset: 6}}>
              <Icon id='setting-btn' onClick={() => this.setState({isEditing: true})} className='notify-setting-btn-hidden' type="setting"/>
              <Tabs defaultActiveKey="1" style={{margin: '30px 0'}}>
                <TabPane tab="Уведомления" key="1" style={{margin: '30px 0'}}>
                  {localStorage.getItem('withoutServer') === 'true' ?
                    (<div>
                        {notifies.reverse().map(item =>
                          <NotifyCard key={item.id} {...item}/>
                        )}
                      </div>
                    )
                    :
                    <div>
                      {this.props.notifies.map((item, index) =>
                        <NotifyCard key={index} {...item}/>
                      )}
                    </div>
                  }
                </TabPane>
                <TabPane tab="Приглашения" key="2" style={{margin: '30px 0'}}>
                  {localStorage.getItem('withoutServer') === 'true' ?
                    (<div>
                        {invites.reverse().map(item =>
                          <InviteCard key={item.id} {...item}/>
                        )}
                      </div>
                    )
                    :
                    (<div>
                        {this.props.invites.map(item =>
                          <InviteCard key={item.id} {...item}/>
                        )}
                      </div>
                    )
                  }
                </TabPane>
              </Tabs>
            </Col>
            :
            <Col xs={{span: 22, offset: 1}} sm={{span: 20, offset: 2}} lg={{span: 12, offset: 6}}>
              <Row style={{textAlign: 'center', marginTop: 40}}><h4 style={{marginBottom: 0}}>Настройка уведомлений</h4></Row>
              <Row><Divider/></Row>
              <Row style={{marginTop: 0}} className='notify-settings'>
                <FormItem
                  labelCol={{
                    xs: { span: 24 },
                    sm: { span: 14 }
                  }}
                  colon={false}
                  label="Уведомления о завершении курса"
                >
                  <Col className='xs-text-align-left'>
                    <Select value={getNotifySettingType(this.state.CourseFinished)} onChange={(e) => this.onHandleSettingChange('CourseFinished', e)} style={{width: 180}}>
                      <Option value="DontSend">Не отправлять</Option>
                      <Option value="ToMail">На почту</Option>
                      <Option value="OnSite">На сайте</Option>
                      <Option value="Everywhere">Везде</Option>
                    </Select>
                  </Col>
                </FormItem>
                <FormItem
                  labelCol={{
                    xs: { span: 24 },
                    sm: { span: 14 }
                  }}
                  colon={false}
                  label="Уведомления о принятии плана курса"
                >
                  <Col className='xs-text-align-left'>
                    <Select value={getNotifySettingType(this.state.CurriculumAccepted)} onChange={(e) => this.onHandleSettingChange('CurriculumAccepted', e)} style={{width: 180}}>
                      <Option value="DontSend">Не отправлять</Option>
                      <Option value="ToMail">На почту</Option>
                      <Option value="OnSite">На сайте</Option>
                      <Option value="Everywhere">Везде</Option>
                    </Select>
                  </Col>
                </FormItem>
                <FormItem
                  labelCol={{
                    xs: { span: 24 },
                    sm: { span: 14 }
                  }}
                  colon={false}
                  label="Уведомления об отклонении плана курса"
                >
                  <Col className='xs-text-align-left'>
                    <Select value={getNotifySettingType(this.state.CurriculumDeclined)} onChange={(e) => this.onHandleSettingChange('CurriculumDeclined', e)} style={{width: 180}}>
                      <Option value="DontSend">Не отправлять</Option>
                      <Option value="ToMail">На почту</Option>
                      <Option value="OnSite">На сайте</Option>
                      <Option value="Everywhere">Везде</Option>
                    </Select>
                  </Col>
                </FormItem>
                <FormItem
                  labelCol={{
                    xs: { span: 24 },
                    sm: { span: 14 }
                  }}
                  colon={false}
                  label="Уведомления о предложении плана курса"
                >
                  <Col className='xs-text-align-left'>
                    <Select value={getNotifySettingType(this.state.CurriculumSuggested)} onChange={(e) => this.onHandleSettingChange('CurriculumSuggested', e)} style={{width: 180}}>
                      <Option value="DontSend">Не отправлять</Option>
                      <Option value="ToMail">На почту</Option>
                      <Option value="OnSite">На сайте</Option>
                      <Option value="Everywhere">Везде</Option>
                    </Select>
                  </Col>
                </FormItem>
                <FormItem
                  labelCol={{
                    xs: { span: 24 },
                    sm: { span: 14 }
                  }}
                  colon={false}
                  label="Уведомления о формировании группы"
                >
                  <Col className='xs-text-align-left'>
                    <Select value={getNotifySettingType(this.state.GroupIsFormed)} onChange={(e) => this.onHandleSettingChange('GroupIsFormed', e)} style={{width: 180}}>
                      <Option value="DontSend">Не отправлять</Option>
                      <Option value="ToMail">На почту</Option>
                      <Option value="OnSite">На сайте</Option>
                      <Option value="Everywhere">Везде</Option>
                    </Select>
                  </Col>
                </FormItem>
                <FormItem
                  labelCol={{
                    xs: { span: 24 },
                    sm: { span: 14 }
                  }}
                  colon={false}
                  label="Уведомления о принятии приглашения"
                >
                  <Col className='xs-text-align-left'>
                    <Select value={getNotifySettingType(this.state.InvitationAccepted)} onChange={(e) => this.onHandleSettingChange('InvitationAccepted', e)} style={{width: 180}}>
                      <Option value="DontSend">Не отправлять</Option>
                      <Option value="ToMail">На почту</Option>
                      <Option value="OnSite">На сайте</Option>
                      <Option value="Everywhere">Везде</Option>
                    </Select>
                  </Col>
                </FormItem>
                <FormItem
                  labelCol={{
                    xs: { span: 24 },
                    sm: { span: 14 }
                  }}
                  colon={false}
                  label="Уведомления об откланении приглашения"
                >
                  <Col className='xs-text-align-left'>
                    <Select value={getNotifySettingType(this.state.InvitationDeclined)} onChange={(e) => this.onHandleSettingChange('InvitationDeclined', e)} style={{width: 180}}>
                      <Option value="DontSend">Не отправлять</Option>
                      <Option value="ToMail">На почту</Option>
                      <Option value="OnSite">На сайте</Option>
                      <Option value="Everywhere">Везде</Option>
                    </Select>
                  </Col>
                </FormItem>
                <FormItem
                  labelCol={{
                    xs: { span: 24 },
                    sm: { span: 14 }
                  }}
                  colon={false}
                  label="Уведомления о получении приглашения"
                >
                  <Col className='xs-text-align-left'>
                    <Select value={getNotifySettingType(this.state.InvitationReceived)} onChange={(e) => this.onHandleSettingChange('InvitationReceived', e)} style={{width: 180}}>
                      <Option value="DontSend">Не отправлять</Option>
                      <Option value="ToMail">На почту</Option>
                      <Option value="OnSite">На сайте</Option>
                      <Option value="Everywhere">Везде</Option>
                    </Select>
                  </Col>
                </FormItem>
                <FormItem
                  labelCol={{
                    xs: { span: 24 },
                    sm: { span: 14 }
                  }}
                  colon={false}
                  label="Уведомления о выходе из группы"
                >
                  <Col className='xs-text-align-left'>
                    <Select value={getNotifySettingType(this.state.MemberLeft)} onChange={(e) => this.onHandleSettingChange('MemberLeft', e)} style={{width: 180}}>
                      <Option value="DontSend">Не отправлять</Option>
                      <Option value="ToMail">На почту</Option>
                      <Option value="OnSite">На сайте</Option>
                      <Option value="Everywhere">Везде</Option>
                    </Select>
                  </Col>
                </FormItem>
                <FormItem
                  labelCol={{
                    xs: { span: 24 },
                    sm: { span: 14 }
                  }}
                  colon={false}
                  label="Уведомления о вступлении в группу"
                >
                  <Col className='xs-text-align-left'>
                    <Select value={getNotifySettingType(this.state.NewMember)} onChange={(e) => this.onHandleSettingChange('NewMember', e)} style={{width: 180}}>
                      <Option value="DontSend">Не отправлять</Option>
                      <Option value="ToMail">На почту</Option>
                      <Option value="OnSite">На сайте</Option>
                      <Option value="Everywhere">Везде</Option>
                    </Select>
                  </Col>
                </FormItem>
                <FormItem
                  labelCol={{
                    xs: { span: 24 },
                    sm: { span: 14 }
                  }}
                  colon={false}
                  label="Уведомления о получении отзыва"
                >
                  <Col className='xs-text-align-left'>
                    <Select value={getNotifySettingType(this.state.ReviewReceived)} onChange={(e) => this.onHandleSettingChange('ReviewReceived', e)} style={{width: 180}}>
                      <Option value="DontSend">Не отправлять</Option>
                      <Option value="ToMail">На почту</Option>
                      <Option value="OnSite">На сайте</Option>
                      <Option value="Everywhere">Везде</Option>
                    </Select>
                  </Col>
                </FormItem>
                <FormItem
                  labelCol={{
                    xs: { span: 24 },
                    sm: { span: 14 }
                  }}
                  colon={false}
                  label="Уведомления, если к вам применили санкцию"
                >
                  <Col className='xs-text-align-left'>
                    <Select value={getNotifySettingType(this.state.SanctionsAppliedToUser)} onChange={(e) => this.onHandleSettingChange('SanctionsAppliedToUser', e)} style={{width: 180}}>
                      <Option value="DontSend">Не отправлять</Option>
                      <Option value="ToMail">На почту</Option>
                      <Option value="OnSite">На сайте</Option>
                      <Option value="Everywhere">Везде</Option>
                    </Select>
                  </Col>
                </FormItem>
                <FormItem
                  labelCol={{
                    xs: { span: 24 },
                    sm: { span: 14 }
                  }}
                  colon={false}
                  label="Уведомления о вступлении учителя в группу"
                >
                  <Col className='xs-text-align-left'>
                    <Select value={getNotifySettingType(this.state.TeacherFound)} onChange={(e) => this.onHandleSettingChange('TeacherFound', e)} style={{width: 180}}>
                      <Option value="DontSend">Не отправлять</Option>
                      <Option value="ToMail">На почту</Option>
                      <Option value="OnSite">На сайте</Option>
                      <Option value="Everywhere">Везде</Option>
                    </Select>
                  </Col>
                </FormItem>
                <FormItem
                  labelCol={{
                    xs: { span: 24 },
                    sm: { span: 14 }
                  }}
                  colon={false}
                  label="Уведомления, если санкцию, примененную к вам, отменили"
                >
                  <Col className='xs-text-align-left'>
                    <Select value={getNotifySettingType(this.state.SanctionsCancelledToUser)} onChange={(e) => this.onHandleSettingChange('SanctionsCancelledToUser', e)} style={{width: 180}}>
                      <Option value="DontSend">Не отправлять</Option>
                      <Option value="ToMail">На почту</Option>
                      <Option value="OnSite">На сайте</Option>
                      <Option value="Everywhere">Везде</Option>
                    </Select>
                  </Col>
                </FormItem>
                <Col style={{marginTop: 20, textAlign: 'center', marginBottom: 40}}>
                  <Button onClick={() => this.setState({isEditing: false})} style={{minWidth: 200}}>Назад</Button>
                </Col>
              </Row>
            </Col>
        }
      </Row>
    );
  }
}

NotificationPage.propTypes = {
  dispatch: PropTypes.func,
  settings: PropTypes.object
};

NotificationPage.defaultProps = {
  settings: {
    CourseFinished: 4,
    CurriculumAccepted: 4,
    CurriculumDeclined: 4,
    CurriculumSuggested: 4,
    GroupIsFormed: 4,
    InvitationAccepted: 4,
    InvitationDeclined: 4,
    InvitationReceived: 4,
    MemberLeft: 4,
    NewCreator: 4,
    NewMember: 4,
    ReviewReceived: 4,
    SanctionsAppliedToUser: 4,
    SanctionsCancelledToUser: 4,
    TeacherFound: 4
  }
};

const mapStateToProps = createStructuredSelector({
  notifies: makeSelectNotifies(),
  invites: makeSelectInvites(),
  needUpdate: makeSelectNeedUpdate()
});

function mapDispatchToProps(dispatch) {
  return {
    getNotifies: () => dispatch(getNotifies()),
    getInvites: () => dispatch(getInvites()),
    setNotifySetting: (notifyType, settingType) => dispatch(setNotifySetting(notifyType, settingType))
  }
}

const withConnect = connect(mapStateToProps, mapDispatchToProps);

const withReducer = injectReducer({ key: 'notificationPage', reducer });
const withSaga = injectSaga({ key: 'notificationPage', saga });

export default compose(
  withReducer,
  withSaga,
  withConnect,
)(NotificationPage);
