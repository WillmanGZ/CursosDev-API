import { Routes } from '@angular/router';
import { LayoutComponent } from './shared/components';
import { authGuard } from './core/auth/auth.guard';

export const routes: Routes = [
  {
    path: 'auth',
    loadChildren: () => import('./features/auth/auth.routes').then((m) => m.AUTH_ROUTES),
  },
  {
    path: '',
    component: LayoutComponent,
    canActivate: [authGuard],
    children: [
      {
        path: 'courses',
        loadChildren: () =>
          import('./features/courses/courses.routes').then((m) => m.COURSES_ROUTES),
      },
      {
        path: 'lessons',
        loadChildren: () =>
          import('./features/lessons/lessons.routes').then((m) => m.LESSONS_ROUTES),
      },
      {
        path: '',
        redirectTo: 'courses',
        pathMatch: 'full',
      },
    ],
  },
  {
    path: '**',
    redirectTo: 'courses',
  },
];
