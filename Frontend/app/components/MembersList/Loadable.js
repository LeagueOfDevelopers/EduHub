/**
 *
 * Asynchronously loads the component for MembersList
 *
 */

import Loadable from 'react-loadable';

export default Loadable({
  loader: () => import('./index'),
  loading: () => null,
});
