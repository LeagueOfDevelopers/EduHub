/**
*
* InviteMemberSelect
*
*/

import React from 'react';
import PropTypes from 'prop-types';
import { connect } from 'react-redux';
import { createStructuredSelector } from 'reselect';
import { inviteMember } from "../../containers/GroupPage/actions";
import { getUsers } from "../../containers/Header/actions";
import { makeSelectUsers } from "../../containers/Header/selectors";
import {Dropdown, Button, Menu, Select, message, Col} from 'antd';


class InviteMemberSelect extends React.Component { // eslint-disable-line react/prefer-stateless-function
  constructor(props) {
    super(props);

    this.state = {
      inviteVisible: false,
      selectValue: '',
      inviteMemberRole: null
    };

    this.tryInviteMember = this.tryInviteMember.bind(this);
    this.handleVisibleChange = this.handleVisibleChange.bind(this);
    this.handleSelectChange = this.handleSelectChange.bind(this);
  }

  handleVisibleChange = (flag) => {
    this.setState({
      inviteVisible: flag
    });
    setTimeout(() => this.setState({
      inviteMemberRole: ''
    }), 300);
  };

  handleSelectChange = (value) => {
    this.setState({selectValue: value});
    setTimeout(() => this.props.getUsers(this.state.selectValue), 0)
  };

  tryInviteMember(invitedId) {
    if(localStorage.getItem('without_server') !== 'true') {
      if(this.state.inviteMemberRole === 'Teacher') {
        this.props.inviteMember(this.props.groupId, invitedId, 'Teacher')
      }
      else if(this.state.inviteMemberRole === 'Member') {
        this.props.inviteMember(this.props.groupId, invitedId, 'Member')
      }
    }
    else {
      message.success('Приглашение отправлено');
    }

    setTimeout(() => this.setState({selectValue: ''}), 0);
  }

  render() {
    return (
      <Dropdown
        overlay={(
          <Menu>
            {!this.state.inviteMemberRole ?
              <Menu.Item className='unhover' key='0'>
                <Button onClick={() => this.setState({inviteMemberRole: 'Member'})} style={{display: 'block', width: '100%', marginBottom: 1}}>Участник</Button>
                <Button onClick={() => this.setState({inviteMemberRole: 'Teacher'})} style={{display: 'block', width: '100%'}}>Учитель</Button>
              </Menu.Item>
              :
              <Menu.Item className='unhover' key='1'>
                <Select
                  mode='combobox'
                  className='unhover'
                  style={{width: '100%'}}
                  value={this.state.selectValue}
                  onChange={this.handleSelectChange}
                  placeholder='Введите имя пользователя'
                  defaultActiveFirstOption={false}
                  showArrow={false}
                  onSelect={(e) => console.log(e)}
                >
                  {this.props.users.map(item =>
                    <Select.Option key={item.name}>
                      <div onClick={() => this.tryInviteMember(item.id)}>{item.name}</div>
                    </Select.Option>)}
                </Select>
              </Menu.Item>
            }
          </Menu>
        )}
        onVisibleChange={this.handleVisibleChange}
        visible={this.state.inviteVisible}
        trigger={['click']}
      >
        <Button
          className='md-offset-16px'
          size='large'
          style={{width: 280}}
          type='primary'
        >
          Пригласить
        </Button>
      </Dropdown>
    );
  }
}

InviteMemberSelect.propTypes = {

};

InviteMemberSelect.defaultProps = {
  users: localStorage.getItem('withoutServer') === 'true' ?
    ['Первый пользователь', 'Второй пользователь'] : []
};

const mapStateToProps = createStructuredSelector({
  users: makeSelectUsers()
});

function mapDispatchToProps(dispatch) {
  return {
    getUsers: (name) => dispatch(getUsers(name)),
    inviteMember: (groupId, invitedId, role) => dispatch(inviteMember(groupId, invitedId, role))
  };
}

const withConnect = connect(mapStateToProps, mapDispatchToProps);

export default withConnect(InviteMemberSelect);
