/**
 *
 * EnterGroupBtn
 *
 */

import React from 'react';
import PropTypes from 'prop-types';
import { connect } from 'react-redux';
import { createStructuredSelector } from 'reselect';
import {
  enterGroup,
  leaveGroup
} from "../../containers/GroupPage/actions";
import {Dropdown, Button, Menu, Select, message, Col, Row} from 'antd';


class EnterGroupBtn extends React.Component { // eslint-disable-line react/prefer-stateless-function
  constructor(props) {
    super(props);

    this.state = {
      roleVisible: false
    };

    this.handleVisibleChange = this.handleVisibleChange.bind(this);
  }

  handleVisibleChange = (flag) => {
    this.setState({
      roleVisible: flag
    });
  };

  render() {
    return (
      <div>
        {this.props.courseStatus !== 3 && this.props.courseStatus !== 2 ?
          this.props.isInGroup ?
            (<Row className='lg-center-container-item'>
                <Button className='group-btn' onClick={() => {
                  this.props.leaveGroup(this.props.groupId, this.props.userData.UserId, this.props.isTeacher ? 'Teacher' : 'Member')
                }}
                >
                  Покинуть группу
                </Button>
              </Row>
            )
            : this.props.memberAmount < this.props.size || localStorage.getItem('isTeacher') === 'true' && !this.props.members.find(item => item.role === 3) ?
              this.props.memberAmount < this.props.size && localStorage.getItem('isTeacher') === 'true' && !this.props.members.find(item => item.role === 3) ?
                <Row className='lg-center-container-item'>
                  <Dropdown
                    overlay={(
                      <Menu>
                        <Menu.Item className='unhover' key='0'>
                          <Button
                            onClick={() => {
                              this.setState({roleVisible: false});
                              this.props.enterGroup(this.props.groupId, 'Member');
                              if (!this.props.userData) {
                                this.props.onSignInClick()
                              }
                            }}
                            style={{display: 'block', width: '100%', marginBottom: 1}}
                          >
                            Участник
                          </Button>
                          <Button
                            onClick={() => {
                              this.setState({roleVisible: false});
                              this.props.enterGroup(this.props.groupId, 'Teacher');
                              if (!this.props.userData) {
                                this.props.onSignInClick()
                              }
                            }}
                            style={{display: 'block', width: '100%'}}
                          >
                            Учитель
                          </Button>
                        </Menu.Item>
                      </Menu>
                    )}
                    onVisibleChange={this.handleVisibleChange}
                    visible={this.state.roleVisible}
                    trigger={['click']}
                    placement="bottomRight"
                    style={{width: 146}}
                  >

                      <Button type='primary' className='group-btn'>
                        Вступить в группу
                      </Button>
                  </Dropdown>
                </Row>
            : this.props.memberAmount < this.props.size || localStorage.getItem('isTeacher') !== 'true' ?
            <Row className='lg-center-container-item'>
              <Button type='primary' className='group-btn' onClick={() => {
                this.props.enterGroup(this.props.groupId, 'Member');
                if (!this.props.userData) {
                  this.props.onSignInClick()
                }
              }}>
                Вступить в группу
              </Button>
            </Row>
            : localStorage.getItem('isTeacher') === 'true' && !this.props.members.find(item => item.role === 3) ?
            <Row className='lg-center-container-item'>
              <Button type='primary' className='group-btn' onClick={() => {
                this.props.enterGroup(this.props.groupId, 'Teacher');
                if (!this.props.userData) {
                  this.props.onSignInClick()
                }
              }}>
                Вступить в группу
              </Button>
            </Row>
                  : null
            : null
          : null
        }
      </div>
    );
  }
}

EnterGroupBtn.propTypes = {

};

EnterGroupBtn.defaultProps = {

};

const mapStateToProps = createStructuredSelector({

});

function mapDispatchToProps(dispatch) {
  return {
    enterGroup: (groupId, role) => dispatch(enterGroup(groupId, role)),
    leaveGroup: (groupId, memberId, role) => dispatch(leaveGroup(groupId, memberId, role))
  };
}

const withConnect = connect(mapStateToProps, mapDispatchToProps);

export default withConnect(EnterGroupBtn);
