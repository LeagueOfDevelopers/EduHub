/**
 *
 * CreateGroupPage
 *
 */

import React from 'react';
import PropTypes from 'prop-types';
import { connect } from 'react-redux';
import { createStructuredSelector } from 'reselect';
import { compose } from 'redux';
import injectSaga from 'utils/injectSaga';
import injectReducer from 'utils/injectReducer';
import reducer from './reducer';
import saga from './saga';
import {createGroup, getTags} from "./actions";
import { makeSelectTags } from "./selectors";
import { Form, Col, Row, Button, Divider, message, Input, Switch, Select, InputNumber } from 'antd';
const FormItem = Form.Item;
const Option = Select.Option;
const {TextArea} = Input;

const tailFormItemLayout = {
  wrapperCol: {
    xs: {
      span: 24,
      offset: 0,
    },
    sm: {
      span: 24,
      offset: 0,
    },
  }
};

const formItemLayout = {
  labelCol: {
    xs: { span: 24 },
    sm: { span: 4, offset: 5 },
    md: { offset: 6 }
  },
  wrapperCol: {
    xs: { span: 24 },
    sm: { span: 8 }
  },
};

export class CreateGroupPage extends React.PureComponent { // eslint-disable-line react/prefer-stateless-function
  constructor(props) {
    super(props);

    this.state = {
      members: [],
      title: '',
      size: null,
      techs: [],
      type: '',
      description: '',
      price: '',
      isPrivate: false
    };

    this.createGroup = this.createGroup.bind(this);
    this.goBack = this.goBack.bind(this);
    this.onHandleTitleChange = this.onHandleTitleChange.bind(this);
    this.onHandleSizeChange = this.onHandleSizeChange.bind(this);
    this.onHandleTechsChange = this.onHandleTechsChange.bind(this);
    this.onHandleSearch = this.onHandleSearch.bind(this);
    this.onHandleDescChange = this.onHandleDescChange.bind(this);
    this.onHandleTypeChange = this.onHandleTypeChange.bind(this);
    this.onHandlePriceChange = this.onHandlePriceChange.bind(this);
    this.onHandlePrivateChange = this.onHandlePrivateChange.bind(this);
    this.handleSubmit = this.handleSubmit.bind(this);
    this.validateTagsInput = this.validateTagsInput.bind(this);
  }

  createGroup = () => {
    (localStorage.getItem('without_server') !== 'true') ?
      this.props.createGroup(
        this.state.title,
        this.state.description,
        this.state.techs,
        this.state.size,
        this.state.price,
        this.state.type,
        this.state.isPrivate
      ) : null
  };

  goBack = () => {
    history.back()
  };

  onHandleTitleChange = (e) => {
    this.setState({title: e.target.value})
  };

  onHandleSizeChange = (e) => {
    this.setState({size: e})
  };

  onHandleTechsChange = (e) => {
    this.setState({techs: e});
  };

  onHandleSearch = (e) => {
    this.props.getTags(e);
  };

  onHandleTypeChange = (e) => {
    this.setState({type: e})
  };

  onHandleDescChange = (e) => {
    this.setState({description: e.target.value})

  };

  onHandlePriceChange = (e) => {
    this.setState({price: e.target.value})
  };

  onHandlePrivateChange = (e) => {
    this.setState({isPrivate: e})
  };

  validateTagsInput = (rule, value, callback) => {
    if (value.length < 3) {
      callback('Должно быть не менее 3 тегов!')
    }
    else if (value.length > 10) {
      callback('Должно быть не более 10 тегов!')
    }
    else if (value.filter(item => item.length > 16).length !== 0) {
      callback('В теге не может быть более 16 символов!')
    }
    else {
      callback()
    }
  };

  handleSubmit = (e) => {
    e.preventDefault();
    this.props.form.validateFields((err, values) => {
      if (!err) {
        this.createGroup(values.title, values.desc , values.tags , values.size, values.price, values.type, values.privacy);
      }
    });
  };

  render() {
    const { getFieldDecorator } = this.props.form;
    return (
      <div style={{marginBottom: 90}}>
        <Row style={{textAlign: 'center', marginTop: 40}}><h3 style={{marginBottom: 0}}>Создание группы</h3></Row>
        <Row><Divider/></Row>
        <Row style={{marginTop: 0}}>
          <Form onSubmit={this.handleSubmit} hideRequiredMark={true} className='form'>
            <FormItem
              {...formItemLayout}
              label="Название группы"
            >
              {getFieldDecorator('title', {
                rules: [
                  {required: true, message: 'Пожалуйста, введите название группы!'},
                  {min: 3, message: 'Название должно содержать не менее 3 символов!'},
                  {max: 70, message: 'Название должно содержать не более 70 символов!'}
                ],
                initialValue: this.state.title
              })(
                <Input onChange={this.onHandleTitleChange} placeholder="Введите название группы"/>)
              }
            </FormItem>
            <FormItem
              {...formItemLayout}
              label="Человек в группе"
            >
              {getFieldDecorator('size', {
                rules: [{required: true, message: 'Пожалуйста, введите количество человек!'}],
                initialValue: this.state.size
              })(
                <InputNumber onChange={this.onHandleSizeChange} min={1} max={200} placeholder="1"/>)
              }
            </FormItem>
            <FormItem
              {...formItemLayout}
              label="Изучаемые технологии"
            >
              {getFieldDecorator('tags', {
                rules: [
                  {required: true, message: 'Пожалуйста, введите изучаемые технологии!'},
                  {validator: this.validateTagsInput}
                ],
                initialValue: this.state.techs
              })(
                <Select onChange={this.onHandleTechsChange} onSearch={this.onHandleSearch} mode="tags" placeholder="Введите, что хотите изучить" notFoundContent={null}>
                  {this.props.tags.length && this.props.tags.length !== 0 ?
                    this.props.tags.map((item, index) =>
                    <Option key={item.tag}>{item.tag}</Option>
                  ) : null}
                </Select>)
              }
            </FormItem>
            <FormItem
              {...formItemLayout}
              label="Формат занятий"
            >
              {/*<Col span={24}>*/}
                {getFieldDecorator('type', {
                  rules: [{required: true, message: 'Пожалуйста, выберите формат обучения!'}],
                  initialValue: this.state.type
                })(
                  <Select onChange={this.onHandleTypeChange} placeholder="Выберите формат">
                    <Option value="Lecture">Лекция</Option>
                    <Option value="MasterClass">Мастер-класс</Option>
                    <Option value="Seminar">Семинар</Option>
                  </Select>)
                }
              {/*</Col>*/}
            </FormItem>
            <FormItem
              {...formItemLayout}
              label="Описание группы"
            >
              {getFieldDecorator('desc', {
                rules: [
                  {required: true, message: 'Пожалуйста, введите описание!'},
                  {min: 20, message: 'Должно быть не менее 20 символов!'},
                  {max: 3000, message: 'Должно быть не более 3000 символов!'}
                ],
                initialValue: this.state.description
              })(
                <TextArea onChange={this.onHandleDescChange} rows={4}/>)
              }
            </FormItem>
            <FormItem
              {...formItemLayout}
              label="Стоимость"
            >
              {/*<Col span={24}>*/}
                {getFieldDecorator('price', {
                  rules: [
                    {required: true, message: 'Пожалуйста, введите стоимость занятия в рублях!'},
                    {pattern: /^[\d]+$/, message: 'Пожалуйста, введите числовое значение в рублях!'}
                  ],
                  initialValue: this.state.price
                })(
                  <Input onChange={this.onHandlePriceChange} addonAfter='руб.'/>)
                }
              {/*</Col>*/}
            </FormItem >
            {/*<FormItem*/}
              {/*{...formItemLayout}*/}
              {/*label="Добавить участника"*/}
            {/*>*/}
              {/*<Select*/}
                {/*mode="multiple"*/}
                {/*defaultActiveFirstOption={false}*/}
                {/*placeholder="Добавьте участников"*/}
              {/*>*/}
                {/*{this.state.members.map(item =>*/}
                  {/*<Option key={item.id}>{item.name}</Option>*/}
                {/*)}*/}
              {/*</Select>*/}
            {/*</FormItem >*/}
            <FormItem
              {...formItemLayout}
              label="Приватная группа"
            >
              {getFieldDecorator('privacy', {
                initialValue: this.state.isPrivate
              })(
                <Switch onChange={this.onHandlePrivateChange}/>)
              }
            </FormItem >
            <Col offset={10} className='sm-row-center' style={{marginTop: 20}}>
              <FormItem {...tailFormItemLayout}>
                <Button htmlType="button" style={{marginRight: '2%'}} onClick={this.goBack}>Отменить</Button>
                <Button type="primary" htmlType="submit">Создать группу</Button>
              </FormItem>
            </Col>
          </Form>
        </Row>
      </div>
    );
  }
}

CreateGroupPage.propTypes = {
  dispatch: PropTypes.func,
  createGroup: PropTypes.func,
  goBack: PropTypes.func,
  members: PropTypes.array,
  title: PropTypes.string,
  size: PropTypes.number,
  techs: PropTypes.array,
  type: PropTypes.string,
  description: PropTypes.string,
  price: PropTypes.string,
  isPrivate: PropTypes.bool
};

const mapStateToProps = createStructuredSelector({
  tags: makeSelectTags()
});

function mapDispatchToProps(dispatch) {
  return {
    createGroup: (title, desc, tags, size, moneyPerUser, groupType, isPrivate) => dispatch(createGroup(title, desc, tags, size, moneyPerUser, groupType, isPrivate)),
    getTags: (tag) => dispatch(getTags(tag))
  };
}

const withConnect = connect(mapStateToProps, mapDispatchToProps);

const withReducer = injectReducer({ key: 'createGroupPage', reducer });
const withSaga = injectSaga({ key: 'createGroupPage', saga });

export default compose(
  withReducer,
  withSaga,
  withConnect,
)(Form.create()(CreateGroupPage));
