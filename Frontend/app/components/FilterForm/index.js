/**
*
* FilterForm
*
*/

import React from 'react';
import {Row, Col, Card, Input, Select, Radio, Checkbox, Divider} from 'antd';


class FilterForm extends React.Component { // eslint-disable-line react/prefer-stateless-function
  constructor(props) {
    super(props);

    this.state = {
      username: '',
      role: 'any',
      skills: [],
      teacherExperience: 'any',
      studentExperience: 'any',
      teacherRateStart: 'none',
      teacherRateEnd: 'none'
    };

    this.onHandleNameChange = this.onHandleNameChange.bind(this);
    this.onHandleRoleChange = this.onHandleRoleChange.bind(this);
    this.onHandleSkillsChange = this.onHandleSkillsChange.bind(this);
    this.onHandleTeacherExperienceChange = this.onHandleTeacherExperienceChange.bind(this);
    this.onHandleStudentExperienceChange = this.onHandleStudentExperienceChange.bind(this);
    this.onHandleTeacherRateStartChange = this.onHandleTeacherRateStartChange.bind(this);
    this.onHandleTeacherRateEndChange = this.onHandleTeacherRateEndChange.bind(this);
  }

  onHandleNameChange = (e) => {
    this.setState({username: e.target.value})
  };

  onHandleRoleChange = (e) => {
    this.setState({role: e.target.value})
  };

  onHandleSkillsChange = (e) => {
    this.setState({skills: e})
  };

  onHandleTeacherExperienceChange = (e) => {
    this.setState({teacherExperience: e.target.value})
  };

  onHandleStudentExperienceChange = (e) => {
    this.setState({studentExperience: e.target.value})
  };

  onHandleTeacherRateStartChange = (e) => {
    this.setState({teacherRateStart: e})
  };

  onHandleTeacherRateEndChange = (e) => {
    this.setState({teacherRateEnd: e})
  };

  render() {
    return (
      <Col id={this.props.id} xs={{span: 22, offset: 1}} sm={{span: 20, offset: 2}} lg={{span: 7, offset: 2}} xl={{span: 6, offset: 2}} xxl={{span: 5, offset: 2}}>
        <Card
          hoverable
          className='without-border-bottom'
          style={{cursor: 'default'}}
        >
          <Row>
            <div className='margin-bottom-12' style={{fontSize: 16, color: '#000'}}>Имя</div>
            <Input value={this.state.username} onChange={this.onHandleNameChange}/>
          </Row>
          <Divider/>
          <Row>
            <Radio.Group value={this.state.role} onChange={this.onHandleRoleChange}>
              <Radio value='student' style={{display: 'block', lineHeight: '30px'}}>Ученик</Radio>
              <Radio value='teacher' style={{display: 'block', lineHeight: '30px'}}>Преподаватель</Radio>
              <Radio value='any' style={{display: 'block', lineHeight: '30px'}}>Не важно</Radio>
            </Radio.Group>
          </Row>
          <Divider/>
          <Row>
            <div className='margin-bottom-12' style={{fontSize: 16, color: '#000'}}>Навыки</div>
            <Select mode="tags" value={this.state.skills} onChange={this.onHandleSkillsChange} defaultActiveFirstOption={false} style={{width: '100%'}}>
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
              <Radio value='more_1' style={{display: 'block', lineHeight: '30px'}}>Минимум одно занятие</Radio>
              <Radio value='more_5' style={{display: 'block', lineHeight: '30px'}}>Больше пяти занятий</Radio>
              <Radio value='more_10' style={{display: 'block', lineHeight: '30px'}}>Больше десяти занятий</Radio>
              <Radio value='any' style={{display: 'block', lineHeight: '30px'}}>Не важно</Radio>
            </Radio.Group>
          </Row>
          <Divider/>
          <Row>
            <div className='margin-bottom-12' style={{fontSize: 16, color: '#000'}}>Завершено курсов</div>
            <Radio.Group value={this.state.studentExperience} onChange={this.onHandleStudentExperienceChange}>
              <Radio value='none' style={{display: 'block', lineHeight: '30px'}}>Не проходил</Radio>
              <Radio value='more_1' style={{display: 'block', lineHeight: '30px'}}>Минимум один курс</Radio>
              <Radio value='more_5' style={{display: 'block', lineHeight: '30px'}}>Больше пяти курсов</Radio>
              <Radio value='more_15' style={{display: 'block', lineHeight: '30px'}}>Больше пятнадцати курсов</Radio>
              <Radio value='any' style={{display: 'block', lineHeight: '30px'}}>Не важно</Radio>
            </Radio.Group>
          </Row>
          <Divider/>
          <Row>
            <div className='margin-bottom-12' style={{fontSize: 16, color: '#000'}}>Рейтинг преподавателя</div>
            <Select value={this.state.teacherRateStart} onChange={this.onHandleTeacherRateStartChange} style={{ width: 80 }}>
              <Option value="none">От</Option>
              <Option value="1">От 1</Option>
              <Option value="2">От 2</Option>
              <Option value="3">От 3</Option>
              <Option value="4">От 4</Option>
              <Option value="5">От 5</Option>
            </Select>
            <span style={{margin: '0 8px', fontSize: 14}}>–</span>
            <Select value={this.state.teacherRateEnd} onChange={this.onHandleTeacherRateEndChange} style={{ width: 80 }}>
              <Option value="none">До</Option>
              <Option value="1">До 1</Option>
              <Option value="2">До 2</Option>
              <Option value="3">До 3</Option>
              <Option value="4">До 4</Option>
              <Option value="5">До 5</Option>
            </Select>
          </Row>
        </Card>
      </Col>
    );
  }
}

FilterForm.propTypes = {

};

export default FilterForm;
