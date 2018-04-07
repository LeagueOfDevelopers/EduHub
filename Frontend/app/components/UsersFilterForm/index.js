/**
*
* FilterForm
*
*/

import React from 'react';

import { connect } from 'react-redux';
import { createStructuredSelector } from 'reselect';
import { compose } from 'redux';
import { getFilteredUsers } from "../../containers/UsersPage/actions";
import {Row, Col, Card, Input, Select, Radio, Checkbox, Divider} from 'antd';


class UsersFilterForm extends React.Component { // eslint-disable-line react/prefer-stateless-function
  constructor(props) {
    super(props);

    this.state = {
      name: this.props.name ? this.props.name : '',
      wantToTeach: false,
      tags: [],
      teacherExperience: 'Default',
      userExperience: 'Default'
    };

    this.onHandleNameChange = this.onHandleNameChange.bind(this);
    this.onHandleRoleChange = this.onHandleRoleChange.bind(this);
    this.onHandleSkillsChange = this.onHandleSkillsChange.bind(this);
    this.onHandleTeacherExperienceChange = this.onHandleTeacherExperienceChange.bind(this);
    this.onHandleStudentExperienceChange = this.onHandleStudentExperienceChange.bind(this);
    this.onHandleTeacherRateStartChange = this.onHandleTeacherRateStartChange.bind(this);
    this.onHandleTeacherRateEndChange = this.onHandleTeacherRateEndChange.bind(this);
  }

  componentDidMount() {
    this.props.getFilteredUsers(this.state);
  }

  onHandleNameChange = (e) => {
    this.setState({name: e.target.value});
    setTimeout(() => this.props.getFilteredUsers(this.state), 0);
  };

  onHandleRoleChange = (e) => {
    this.setState({wantToTeach: e.target.checked});
    setTimeout(() => this.props.getFilteredUsers(this.state), 0);
  };

  onHandleSkillsChange = (e) => {
    this.setState({tags: e});
    setTimeout(() => this.props.getFilteredUsers(this.state), 0);
  };

  onHandleTeacherExperienceChange = (e) => {
    this.setState({teacherExperience: e.target.value});
    setTimeout(() => this.props.getFilteredUsers(this.state), 0);
  };

  onHandleStudentExperienceChange = (e) => {
    this.setState({userExperience: e.target.value});
    setTimeout(() => this.props.getFilteredUsers(this.state), 0);
  };

  onHandleTeacherRateStartChange = (e) => {
    this.setState({teacherRateStart: e})
  };

  onHandleTeacherRateEndChange = (e) => {
    this.setState({teacherRateEnd: e})
  };

  render() {
    return (
      <Col id={this.props.id} xs={{span: 24}} lg={{span: 7, offset: 2}} xl={{span: 6, offset: 2}} xxl={{span: 5, offset: 2}}>
        <Card
          hoverable
          className='without-border-bottom'
          style={{cursor: 'default', zIndex: 100}}
        >
          <Row>
            <div className='margin-bottom-12' style={{fontSize: 16, color: '#000'}}>Имя</div>
            <Input value={this.state.name} onChange={this.onHandleNameChange}/>
          </Row>
          <Divider/>
          <Row>
            <Checkbox checked={this.state.wantToTeach} onChange={this.onHandleRoleChange}>Преподаватель</Checkbox>
          </Row>
          <Divider/>
          <Row>
            <div className='margin-bottom-12' style={{fontSize: 16, color: '#000'}}>Навыки</div>
            <Select mode="tags" value={this.state.tags} onChange={this.onHandleSkillsChange} defaultActiveFirstOption={false} style={{width: '100%'}}>
              <Select.Option value="html">html</Select.Option>
              <Select.Option value="css">css</Select.Option>
              <Select.Option value="js">js</Select.Option>
              <Select.Option value="c#">c#</Select.Option>
            </Select>
          </Row>
          <Divider/>
          <Row>
            <div className='margin-bottom-12' style={{fontSize: 16, color: '#000'}}>Опыт преподавания</div>
            <Radio.Group value={this.state.teacherExperience} onChange={this.onHandleTeacherExperienceChange}>
              <Radio value='OneClass' style={{display: 'block', lineHeight: '30px'}}>Минимум одно занятие</Radio>
              <Radio value='FiveClasses' style={{display: 'block', lineHeight: '30px'}}>Больше пяти занятий</Radio>
              <Radio value='TenClasses' style={{display: 'block', lineHeight: '30px'}}>Больше десяти занятий</Radio>
              <Radio value='Default' style={{display: 'block', lineHeight: '30px'}}>Не важно</Radio>
            </Radio.Group>
          </Row>
          <Divider/>
          <Row>
            <div className='margin-bottom-12' style={{fontSize: 16, color: '#000'}}>Завершено курсов</div>
            <Radio.Group value={this.state.userExperience} onChange={this.onHandleStudentExperienceChange}>
              {/*<Radio value='none' style={{display: 'block', lineHeight: '30px'}}>Не проходил</Radio>*/}
              <Radio value='OneClass' style={{display: 'block', lineHeight: '30px'}}>Минимум один курс</Radio>
              <Radio value='FiveClasses' style={{display: 'block', lineHeight: '30px'}}>Больше пяти курсов</Radio>
              <Radio value='FifteenClasses' style={{display: 'block', lineHeight: '30px'}}>Больше пятнадцати курсов</Radio>
              <Radio value='Default' style={{display: 'block', lineHeight: '30px'}}>Не важно</Radio>
            </Radio.Group>
          </Row>
          {/*<Divider/>*/}
          {/*<Row>*/}
            {/*<div className='margin-bottom-12' style={{fontSize: 16, color: '#000'}}>Рейтинг преподавателя</div>*/}
            {/*<Select value={this.state.teacherRateStart} onChange={this.onHandleTeacherRateStartChange} style={{ width: 80 }}>*/}
              {/*<Option value="none">От</Option>*/}
              {/*<Option value="1">От 1</Option>*/}
              {/*<Option value="2">От 2</Option>*/}
              {/*<Option value="3">От 3</Option>*/}
              {/*<Option value="4">От 4</Option>*/}
              {/*<Option value="5">От 5</Option>*/}
            {/*</Select>*/}
            {/*<span style={{margin: '0 8px', fontSize: 14}}>–</span>*/}
            {/*<Select value={this.state.teacherRateEnd} onChange={this.onHandleTeacherRateEndChange} style={{ width: 80 }}>*/}
              {/*<Option value="none">До</Option>*/}
              {/*<Option value="1">До 1</Option>*/}
              {/*<Option value="2">До 2</Option>*/}
              {/*<Option value="3">До 3</Option>*/}
              {/*<Option value="4">До 4</Option>*/}
              {/*<Option value="5">До 5</Option>*/}
            {/*</Select>*/}
          {/*</Row>*/}
        </Card>
      </Col>
    );
  }
}

UsersFilterForm.propTypes = {

};

const mapStateToProps = createStructuredSelector({

});

function mapDispatchToProps(dispatch) {
  return {
    getFilteredUsers: (filters) => dispatch(getFilteredUsers(filters))
  }
}

export default connect(mapStateToProps, mapDispatchToProps)(UsersFilterForm);
