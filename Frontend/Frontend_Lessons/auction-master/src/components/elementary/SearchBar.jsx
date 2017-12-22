import React, { Component } from 'react';
import { Input, Select } from 'antd';

const Search = Input.Search;
const { Option, OptGroup } = Select;

function handleChange(value) {
    console.log(`selected ${value}`);
  }

const SearchBar = () => {
        return (
            <Search
                placeholder="Search lots"
                style={{ width: 300, marginRight: '20px', float: 'right' }}
                onSearch={value => console.log(value)}
            />
    );
}

export default SearchBar;