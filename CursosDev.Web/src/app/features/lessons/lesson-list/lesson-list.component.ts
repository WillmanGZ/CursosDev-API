import { Component, inject, OnInit, signal } from '@angular/core';
import { RouterLink } from '@angular/router';
import { LessonService } from '../../../core/services';
import { Lesson } from '../../../shared/models';
import { CommonModule } from '@angular/common';

@Component({
  selector: 'app-lesson-list',
  standalone: true,
  imports: [RouterLink, CommonModule],
  templateUrl: './lesson-list.component.html',
})
export class LessonListComponent implements OnInit {
  private lessonService = inject(LessonService);

  lessons = signal<Lesson[]>([]);
  loading = signal(true);

  ngOnInit() {
    this.loadLessons();
  }

  loadLessons() {
    this.loading.set(true);
    this.lessonService.getAll().subscribe({
      next: (lessons) => {
        this.lessons.set(lessons);
        this.loading.set(false);
      },
      error: () => this.loading.set(false),
    });
  }

  delete(id: string) {
    if (confirm('¿Estás seguro de eliminar esta lección?')) {
      this.lessonService.delete(id).subscribe(() => this.loadLessons());
    }
  }
}
