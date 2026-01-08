import { Component, inject, OnInit, signal } from '@angular/core';
import { FormBuilder, ReactiveFormsModule, Validators } from '@angular/forms';
import { ActivatedRoute, Router, RouterLink } from '@angular/router';
import { CourseService, LessonService } from '../../../core/services';
import { Course } from '../../../shared/models';

@Component({
  selector: 'app-lesson-form',
  standalone: true,
  imports: [ReactiveFormsModule, RouterLink],
  templateUrl: './lesson-form.component.html',
})
export class LessonFormComponent implements OnInit {
  private fb = inject(FormBuilder);
  private lessonService = inject(LessonService);
  private courseService = inject(CourseService);
  private router = inject(Router);
  private route = inject(ActivatedRoute);

  form = this.fb.nonNullable.group({
    courseId: ['', [Validators.required]],
    title: ['', [Validators.required]],
    order: [1, [Validators.required, Validators.min(1)]],
  });

  courses = signal<Course[]>([]);
  loading = signal(false);
  isEdit = signal(false);
  lessonId = '';

  ngOnInit() {
    this.loadCourses();

    const id = this.route.snapshot.paramMap.get('id');
    const courseIdParam = this.route.snapshot.queryParamMap.get('courseId');

    if (courseIdParam) {
      this.form.patchValue({ courseId: courseIdParam });
    }

    if (id && id !== 'new') {
      this.isEdit.set(true);
      this.lessonId = id;
      this.loadLesson(id);
    }
  }

  loadCourses() {
    this.courseService.getAll().subscribe({
      next: (courses) => this.courses.set(courses),
    });
  }

  loadLesson(id: string) {
    this.lessonService.getById(id).subscribe({
      next: (lesson) => {
        this.form.patchValue({
          courseId: lesson.courseId,
          title: lesson.title,
          order: lesson.order,
        });
      },
    });
  }

  onSubmit() {
    if (this.form.invalid) return;

    this.loading.set(true);
    const values = this.form.getRawValue();

    if (this.isEdit()) {
      this.lessonService
        .update(this.lessonId, {
          id: this.lessonId,
          title: values.title,
          order: values.order,
        })
        .subscribe({
          next: () => this.router.navigate(['/lessons']),
          error: () => this.loading.set(false),
        });
    } else {
      this.lessonService.create(values).subscribe({
        next: () => this.router.navigate(['/lessons']),
        error: () => this.loading.set(false),
      });
    }
  }
}
