"use client";

import { CourseClient, CourseMapping } from "../../web-api-client";
import DataTable from "../../../components/DataTable/DataTable";

export default function DashboardCourse() {
  const CourseService = new CourseClient();

  return (
    <DataTable
      url="/api/course"
      fields={Object.keys(new CourseMapping().toJSON())}
      deleteAction={(id) =>
        CourseService.deleteEntity(id)
      }
      fetchAction={(filters, sorts, page, pageSize) =>
        CourseService.getEntities(filters, sorts, page, pageSize)
      }
    />
  );
}
