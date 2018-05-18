/**
 *
 * AdminPage
 *
 */

import React from 'react';
import PropTypes from 'prop-types';
import { connect } from 'react-redux';
import { createStructuredSelector } from 'reselect';
import { compose } from 'redux';
import injectSaga from 'utils/injectSaga';
import injectReducer from 'utils/injectReducer';
import {
  searchUsers,
  inviteModerator,
  deleteModerator,
  applySanction,
  annulSanction,
  getModers,
  getReports,
  getSanctions,
  getAdminHistory
} from "./actions";
import {
  makeSelectUsers,
  makeSelectReports,
  makeSelectSanctions,
  makeSelectModerators,
  makeSelectHistory
} from "./selectors";
import reducer from './reducer';
import saga from './saga';
import { Row, Col, Button, List, Avatar, Icon, Popconfirm, Dropdown, Menu, Select, message } from 'antd';
import {Link} from "react-router-dom";
import ReportModal from '../../components/ReportModal';
import SanctionModal from '../../components/SanctionModal';
import MakeSanctionModal from '../../components/MakeSanctionModal';
import config from "../../config";
import {getSanctionType, parseJwt} from "../../globalJS";

export class AdminPage extends React.Component { // eslint-disable-line react/prefer-stateless-function
  constructor(props) {
    super(props);

    this.state = {
      inviteVisible: false,
      inviteSelectValue: '',
      reportVisible: false,
      sanctionVisible: false,
      makeSanctionVisible: false,
      currentReport: {},
      currentSanction: {},
      userData: localStorage.getItem('token') ? parseJwt(localStorage.getItem('token')) : null
    };

    this.onReportClick = this.onReportClick.bind(this);
    this.handleReportCancel = this.handleReportCancel.bind(this);
    this.onSanctionClick = this.onSanctionClick.bind(this);
    this.handleSanctionCancel = this.handleSanctionCancel.bind(this);
    this.onMakeSanctionClick = this.onMakeSanctionClick.bind(this);
    this.handleMakeSanctionCancel = this.handleMakeSanctionCancel.bind(this);
    this.handleInviteVisibleChange = this.handleInviteVisibleChange.bind(this);
    this.handleInviteSelectChange = this.handleInviteSelectChange.bind(this);
  }

  componentDidMount() {
    this.props.getModers();
    this.props.getReports();
    this.props.getSanctions();
    this.props.getHistory();
  }

  onReportClick = (reportId) => {
    let currentReport = this.props.reports.find(item => item.reportId === reportId) || {};
    this.setState({reportVisible: true, currentReport})
  };

  handleReportCancel = () => {
    this.setState({reportVisible: false})
  };

  onSanctionClick = (sanctionId) => {
    let currentSanction = this.props.sanctions.find(item => item.id === sanctionId) || {};
    this.setState({sanctionVisible: true, currentSanction})
  };

  handleSanctionCancel = () => {
    this.setState({sanctionVisible: false})
  };

  onMakeSanctionClick = () => {
    this.setState({makeSanctionVisible: true, reportVisible: false})
  };

  handleMakeSanctionCancel = () => {
    this.setState({makeSanctionVisible: false})
  };

  handleInviteVisibleChange = (flag) => {
    this.setState({
      inviteVisible: flag
    });
  };

  handleInviteSelectChange = (value) => {
    this.setState({inviteSelectValue: value});
    this.props.getUsers(value);
  };

  render() {
    return (
      this.state.userData && this.props.match.params.id === this.state.userData.UserId && (this.state.userData.Role === 'Admin' || this.state.userData.Role === 'Moderator') ?
        <Col span={20} offset={2} style={{height: 'calc(100% - 64px)', padding: '40px 0'}}>
          <Row type='flex' justify='space-between' align='middle'>
            <Col xs={{span: 24}} md={{span: 14}} lg={{span: 10}} style={{height: 60, fontSize: 22, backgroundColor: 'rgba(0,0,0,0.1)', display: 'flex', justifyContent: 'center', alignItems: 'center', color: 'black'}}>Панель администратора</Col>
            <Col className='sanction-btn' xs={{span: 24}} md={{span: 10}} lg={{span: 14}}><Button onClick={this.onMakeSanctionClick} type='primary' size='large'>Выписать санкцию</Button></Col>
          </Row>
          <Row style={{marginTop: 12}}>
            <Col xs={{span: 24}} md={{span: 10}} xl={{span: 13}}>
              <Col xs={{span: 24}} xl={{span: 11}} style={{boxShadow: 'rgba(0, 0, 0, 0.4) 0px 0px 6px -2px', marginTop: 16}}>
                <List
                  header={(
                    <Row type='flex' justify='space-between'>
                      <Col style={{textAlign: 'center', width: '100%', fontSize: 18}}>Текущие администраторы</Col>
                    </Row>
                  )}
                  className='admin-list'
                  dataSource={this.props.moders}
                  renderItem={item => (
                    <List.Item key={item.id}>
                      <List.Item.Meta
                        avatar={
                          <Avatar
                            src={item.avatarLink ? `${config.API_BASE_URL}/file/img/${item.avatarLink}` : null}
                          />}
                        title={<Link to={`/profile/${item.id}`}>{item.name}</Link>}
                        description={item.email}
                      />
                      <Popconfirm
                        title='Удалить модератора?'
                        onConfirm={() => this.props.deleteModerator(item.id)}
                        okText="Да"
                        cancelText="Нет"
                      >
                        <Icon
                          style={{fontSize: 18, cursor: 'pointer'}}
                          type="close"
                        />
                      </Popconfirm>
                    </List.Item>
                  )}
                  footer={
                    (
                      <Dropdown
                        overlay={(
                          <Menu>
                            <Menu.Item className='unhover'>
                              <Select
                                mode='combobox'
                                className='unhover'
                                style={{width: '100%'}}
                                value={this.state.inviteSelectValue}
                                onChange={this.handleInviteSelectChange}
                                placeholder='Введите имя пользователя'
                                defaultActiveFirstOption={false}
                                showArrow={false}
                              >
                                {this.props.users.map(item =>
                                  <Select.Option key={item.name}>
                                    <div onClick={() => this.props.inviteModerator(item.email)}>{item.name}</div>
                                  </Select.Option>)
                                }
                              </Select>
                            </Menu.Item>
                          </Menu>
                        )}
                        onVisibleChange={this.handleInviteVisibleChange}
                        visible={this.state.inviteVisible}
                        trigger={['click']}
                      >
                        <Button
                          size='large'
                          style={{width: '100%'}}
                        >
                          Пригласить
                        </Button>
                      </Dropdown>
                    )
                  }
                >
                </List>
              </Col>
              <Col className='custom-mw' xs={{span: 24}} xl={{span: 11, offset: 2}} >
                <Col style={{boxShadow: 'rgba(0, 0, 0, 0.4) 0px 0px 6px -2px', marginTop: 16}}>
                  <List
                    header={(
                      <Row type='flex' justify='space-between'>
                        <Col style={{textAlign: 'center', width: '100%', fontSize: 18}}>Текущие репорты</Col>
                      </Row>
                    )}
                    className='admin-list'
                    dataSource={this.props.reports}
                    renderItem={item => (
                      <List.Item key={item.reportId}>
                        <List.Item.Meta
                          title={item.suspectedName}
                          description={item.reason}
                        />
                        <Icon
                          style={{fontSize: 18, cursor: 'pointer'}}
                          type="ellipsis"
                          onClick={() => this.onReportClick(item.reportId)}
                        />
                      </List.Item>
                    )}
                  >
                  </List>
                </Col>
                <Col style={{boxShadow: 'rgba(0, 0, 0, 0.4) 0px 0px 6px -2px', marginTop: 16}}>
                  <List
                    header={(
                      <Row type='flex' justify='space-between'>
                        <Col style={{textAlign: 'center', width: '100%', fontSize: 18}}>Текущие санкции</Col>
                      </Row>
                    )}
                    className='admin-list'
                    dataSource={this.props.sanctions}
                    renderItem={item => (
                      <List.Item key={item.id}>
                        <List.Item.Meta
                          title={<Link to={`/profile/${item.userId}`}>{item.userName}</Link>}
                          description={getSanctionType(item.type)}
                        />
                        <Icon
                          style={{fontSize: 18, cursor: 'pointer'}}
                          type="ellipsis"
                          onClick={() => this.onSanctionClick(item.id)}
                        />
                      </List.Item>
                    )}
                  >
                  </List>
                </Col>
              </Col>
            </Col>
            <Col className='event-list-container' xs={{span: 24}} md={{span: 13, offset: 1}} xl={{span: 10, offset: 1}} style={{boxShadow: 'rgba(0, 0, 0, 0.4) 0px 0px 6px -2px', marginTop: 16, minHeight: 250}}>
              <List
                header={(
                  <Row type='flex' justify='space-between'>
                    <Col style={{textAlign: 'center', width: '100%', fontSize: 18}}>История событий</Col>
                  </Row>
                )}
                className='event-list'
                dataSource={this.props.history}
                renderItem={(item, index) => (
                  <div>
                    <List.Item key={index}>
                      <List.Item.Meta
                        title={item.notificationType === 15 ?
                          `К пользователю ${JSON.parse(item.notificationInfo).Username} была применена санкция "${getSanctionType(JSON.parse(item.notificationInfo).SanctionType)}" за нарушение правила "${JSON.parse(item.notificationInfo).BrokenRule}"`
                          : item.notificationType === 18 ?
                            `Примененная к пользователю ${JSON.parse(item.notificationInfo).Username} санкция "${getSanctionType(JSON.parse(item.notificationInfo).SanctionType)}" за нарушение правила "${JSON.parse(item.notificationInfo).BrokenRule}" была отменена`
                            : ''
                        }
                        description={`${new Date(item.occurredOn).getDate() < 10 ?
                          '0' + new Date(item.occurredOn).getDate()
                          : new Date(item.occurredOn).getDate()}.${new Date(item.occurredOn).getMonth() < 10 ?
                          '0' + new Date(item.occurredOn).getMonth()
                          : new Date(item.occurredOn).getMonth()}.${new Date(item.occurredOn).getFullYear()}
                    ${new Date(item.occurredOn).getHours() < 10 ?
                          '0' + new Date(item.occurredOn).getHours()
                          : new Date(item.occurredOn).getHours()}:${new Date(item.occurredOn).getMinutes() < 10 ?
                          '0' + new Date(item.occurredOn).getMinutes()
                          : new Date(item.occurredOn).getMinutes()}`}
                      >
                      </List.Item.Meta>
                    </List.Item>
                  </div>
                )}
              >
              </List>
            </Col>
          </Row>
          <SanctionModal sanction={this.state.currentSanction} visible={this.state.sanctionVisible} handleCancel={this.handleSanctionCancel}/>
          <MakeSanctionModal visible={this.state.makeSanctionVisible} handleCancel={this.handleMakeSanctionCancel}/>
          <ReportModal report={this.state.currentReport} visible={this.state.reportVisible} onSanctionClick={this.onMakeSanctionClick} handleCancel={this.handleReportCancel}/>
        </Col>
        : location.assign('/access-denied')
    );
  }
}

AdminPage.propTypes = {

};

const mapStateToProps = createStructuredSelector({
  users: makeSelectUsers(),
  moders: makeSelectModerators(),
  reports: makeSelectReports(),
  sanctions: makeSelectSanctions(),
  history: makeSelectHistory()
});

function mapDispatchToProps(dispatch) {
  return {
    getUsers: (name) => dispatch(searchUsers(name)),
    inviteModerator: (email) => dispatch(inviteModerator(email)),
    deleteModerator: (id) => dispatch(deleteModerator(id)),
    annulSanction: (sanctionId) => dispatch(annulSanction(sanctionId)),
    getModers: () => dispatch(getModers()),
    getReports: () => dispatch(getReports()),
    getSanctions: () => dispatch(getSanctions()),
    getHistory: () => dispatch(getAdminHistory()),
  };
}

const withConnect = connect(mapStateToProps, mapDispatchToProps);

const withReducer = injectReducer({ key: 'adminPage', reducer });
const withSaga = injectSaga({ key: 'adminPage', saga });

export default compose(
  withReducer,
  withSaga,
  withConnect,
)(AdminPage);
