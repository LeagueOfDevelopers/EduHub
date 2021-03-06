import { createSelector } from 'reselect';

/**
 * Direct selector to the profilePage state domain
 */
const selectProfilePageDomain = (state) => state.get('profilePage');

/**
 * Other specific selectors
 */


/**
 * Default selector used by ProfilePage
 */

const makeSelectUserGroups = () => createSelector(
  selectProfilePageDomain,
  (profileState) => profileState.get('groups')
);

const makeSelectNeedUpdate = () => createSelector(
  selectProfilePageDomain,
  (profileState) => profileState.get('needUpdate')
);

const makeSelectTags = () => createSelector(
  selectProfilePageDomain,
  (profileState) => profileState.get('tags')
);

export {
  makeSelectUserGroups,
  makeSelectNeedUpdate,
  makeSelectTags
};
